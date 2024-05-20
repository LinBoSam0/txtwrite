using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void InitializeFontComboBox()
        {
            foreach (FontFamily font in FontFamily.Families)
            {
                comboBoxFont.Items.Add(font.Name);
            }
            comboBoxFont.SelectedIndex = 0;
        }

        // 字體大小初始化
        private void InitializeFontSizeComboBox()
        {
            for (int i = 8; i <= 144; i += 2)
            {
                comboBoxSize.Items.Add(i);
            }
            comboBoxSize.SelectedIndex = 2;
        }

        // 字體樣式初始化
        private void InitializeFontStyleComboBox()
        {
            comboBoxStyle.Items.Add(FontStyle.Regular.ToString());
            comboBoxStyle.Items.Add(FontStyle.Bold.ToString());
            comboBoxStyle.Items.Add(FontStyle.Italic.ToString());
            comboBoxStyle.Items.Add(FontStyle.Underline.ToString());
            comboBoxStyle.Items.Add(FontStyle.Strikeout.ToString());
            comboBoxStyle.SelectedIndex = 0;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontSettings();
        }

        private void ApplyFontSettings()
        {
            if (rtbText.SelectionFont != null)
            {
                // 確保選擇的字型、大小和樣式都不為 null
                string selectedFont = comboBoxFont.SelectedItem?.ToString();
                string selectedSizeStr = comboBoxSize.SelectedItem?.ToString();
                string selectedStyleStr = comboBoxStyle.SelectedItem?.ToString();

                if (selectedFont != null && selectedSizeStr != null && selectedStyleStr != null)
                {
                    float selectedSize = float.Parse(selectedSizeStr);
                    FontStyle selectedStyle = (FontStyle)Enum.Parse(typeof(FontStyle), selectedStyleStr);

                    // 建立新的字體
                    Font newFont = new Font(selectedFont, selectedSize, selectedStyle);
                    rtbText.SelectionFont = newFont;
                }
            }
        }

        bool isUndo = false;
        private Stack<string> textHistory = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();
        private const int MaxHistoryCount = 20; // 最多紀錄10個紀錄

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // 設置對話方塊標題
            openFileDialog1.Title = "選擇檔案";
            // 設置對話方塊篩選器，限制使用者只能選擇特定類型的檔案
            openFileDialog1.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            // 如果希望預設開啟的檔案類型是文字檔案，可以這樣設置
            openFileDialog1.FilterIndex = 1;
            // 如果希望對話方塊在開啟時顯示的初始目錄，可以設置 InitialDirectory
            openFileDialog1.InitialDirectory = "C:\\";
            // 允許使用者選擇多個檔案
            openFileDialog1.Multiselect = true;

            // 顯示對話方塊，並等待使用者選擇檔案
            DialogResult result = openFileDialog1.ShowDialog();

            // 檢查使用者是否選擇了檔案
            if (result == DialogResult.OK)
            {
                try
                {
                    // 使用者在OpenFileDialog選擇的檔案
                    string selectedFileName = openFileDialog1.FileName;

                    using (FileStream fileStream = new FileStream(selectedFileName, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                        {
                            rtbText.Text = streamReader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 如果發生錯誤，用MessageBox顯示錯誤訊息
                    MessageBox.Show("讀取檔案時發生錯誤: " + ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("使用者取消了選擇檔案操作。", "訊息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbText.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string saveFilePath = saveFileDialog.FileName;

                        using (StreamWriter writer = new StreamWriter(saveFilePath))
                        {
                            writer.Write(rtbText.Text);
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
            if (textHistory.Count > 1)
            {
                redoStack.Push(textHistory.Pop());
                rtbText.Text = textHistory.Peek();
            }
            UpdateListBox();
            isUndo = false;
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                textHistory.Push(redoStack.Pop());
                rtbText.Text = textHistory.Peek();
                UpdateListBox();
            }
        }

        private void rtbText_TextChanged(object sender, EventArgs e)
        {
            if (!isUndo)
            {
                textHistory.Push(rtbText.Text);
                redoStack.Clear();
                if (textHistory.Count > MaxHistoryCount)
                {
                    Stack<string> tempStack = new Stack<string>();
                    for (int i = 0; i < MaxHistoryCount; i++)
                    {
                        tempStack.Push(textHistory.Pop());
                    }
                    textHistory.Pop();
                    foreach (string item in tempStack)
                    {
                        textHistory.Push(item);
                    }
                }
                UpdateListBox();
            }
        }

        void UpdateListBox()
        {
            listUndo.Items.Clear();
            foreach (string item in textHistory)
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