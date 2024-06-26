﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace txtwrite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeFontComboBox();
            InitializeFontSizeComboBox();
            InitializeFontStyleComboBox();
            AttachComboBoxEventHandlers();
        }

        private void InitializeFontComboBox()
        {
            foreach (FontFamily font in FontFamily.Families)
            {
                comboBoxFont.Items.Add(font.Name);
            }
            comboBoxFont.SelectedIndex = 0;
        }

        private void InitializeFontSizeComboBox()
        {
            for (int i = 8; i <= 144; i += 2)
            {
                comboBoxSize.Items.Add(i);
            }
            comboBoxSize.SelectedIndex = 2;
        }

        private void InitializeFontStyleComboBox()
        {
            comboBoxStyle.Items.Add(FontStyle.Regular.ToString());
            comboBoxStyle.Items.Add(FontStyle.Bold.ToString());
            comboBoxStyle.Items.Add(FontStyle.Italic.ToString());
            comboBoxStyle.Items.Add(FontStyle.Underline.ToString());
            comboBoxStyle.Items.Add(FontStyle.Strikeout.ToString());
            comboBoxStyle.SelectedIndex = 0;
        }

        private void AttachComboBoxEventHandlers()
        {
            comboBoxFont.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            comboBoxSize.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            comboBoxStyle.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
        }

        private int selectionStart = 0; // 記錄文字反白的起點
        private int selectionLength = 0; // 記錄文字反白的長度

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectionStart = rtbText.SelectionStart;
            selectionLength = rtbText.SelectionLength;

            ApplyFontSettings();

            rtbText.Select(selectionStart, selectionLength);
            rtbText.Focus();
        }

        private void ApplyFontSettings()
        {
            if (rtbText.SelectionFont != null)
            {
                string selectedFont = comboBoxFont.SelectedItem?.ToString();
                string selectedSizeStr = comboBoxSize.SelectedItem?.ToString();
                string selectedStyleStr = comboBoxStyle.SelectedItem?.ToString();

                if (selectedFont != null && selectedSizeStr != null && selectedStyleStr != null)
                {
                    float selectedSize = float.Parse(selectedSizeStr);
                    FontStyle selectedStyle = (FontStyle)Enum.Parse(typeof(FontStyle), selectedStyleStr);

                    Font newFont = new Font(selectedFont, selectedSize, selectedStyle);
                    rtbText.SelectionFont = newFont;
                }
            }
        }

        bool isUndo = false;
        private Stack<MemoryStream> undoStack = new Stack<MemoryStream>();
        private Stack<MemoryStream> redoStack = new Stack<MemoryStream>();
        private const int MaxHistoryCount = 20;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "選擇檔案";
            openFileDialog1.Filter = "RTF Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Multiselect = false;

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    string selectedFileName = openFileDialog1.FileName;

                    if (Path.GetExtension(selectedFileName).ToLower() == ".rtf")
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("讀取檔案時發生錯誤: " + ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("使用者取消了選擇檔案操作。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbText.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "RTF Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string saveFilePath = saveFileDialog.FileName;

                        if (Path.GetExtension(saveFilePath).ToLower() == ".rtf")
                        {
                            rtbText.SaveFile(saveFilePath, RichTextBoxStreamType.RichText);
                        }
                        else
                        {
                            rtbText.SaveFile(saveFilePath, RichTextBoxStreamType.PlainText);
                        }

                        MessageBox.Show("檔案已另存新檔。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("儲存檔案時發生錯誤: " + ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("沒有內容需要儲存。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            isUndo = true;
            if (undoStack.Count > 1)
            {
                redoStack.Push(undoStack.Pop());
                LoadFromMemory(undoStack.Peek());
            }
            UpdateListBox();
            isUndo = false;
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(redoStack.Pop());
                LoadFromMemory(undoStack.Peek());
                UpdateListBox();
            }
        }

        private void rtbText_TextChanged(object sender, EventArgs e)
        {
            if (!isUndo)
            {
                SaveCurrentStateToStack();
                redoStack.Clear();

                if (undoStack.Count > MaxHistoryCount)
                {
                    undoStack = new Stack<MemoryStream>(undoStack.Take(MaxHistoryCount));
                }

                UpdateListBox();
            }
        }

        private void SaveCurrentStateToStack()
        {
            MemoryStream memoryStream = new MemoryStream();
            rtbText.SaveFile(memoryStream, RichTextBoxStreamType.RichText);
            undoStack.Push(memoryStream);

            if (undoStack.Count > MaxHistoryCount)
            {
                undoStack = new Stack<MemoryStream>(undoStack.Take(MaxHistoryCount));
            }
        }

        private void LoadFromMemory(MemoryStream memoryStream)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            rtbText.LoadFile(memoryStream, RichTextBoxStreamType.RichText);
        }

        void UpdateListBox()
        {
            listUndo.Items.Clear();

            foreach (MemoryStream item in undoStack)
            {
                listUndo.Items.Add(item);
            }
        }

        private void listUndo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optionally handle selection change
        }
    }
}