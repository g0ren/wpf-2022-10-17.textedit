using Microsoft.VisualBasic;
using System.IO;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace _10._17.textedit
{
    public partial class Form1 : Form
    {
        public bool FileIsOpen { get; set; } = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadFile(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text=openFileDialog1.FileName;
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                StringBuilder sb=new StringBuilder();
                foreach(string line in lines)
                {
                    sb.AppendLine(line);
                }
                textBox1.Text = sb.ToString();
                FileIsOpen = true;
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            if (FileIsOpen)
            {
                System.IO.File.WriteAllText(this.Text, textBox1.Text);
            }
            else
            {
                SaveFileAs(sender, e);
            }
        }

        private void SaveFileAs(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
            this.Text = saveFileDialog1.FileName;
            FileIsOpen = true;
        }

        private void NewFile(object sender, EventArgs e)
        {
            this.Text = "";
            textBox1.Text = "";
            FileIsOpen = false;
        }

        private void CopyText(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void CutText(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
        }

        private void PasteText(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, Clipboard.GetText());
        }

        private void Undo(object sender, EventArgs e)
        {
            textBox1.Undo();
            textBox1.ClearUndo();
        }

        private void TextOptions(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void BackgroundOptions(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox1.BackColor; 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
               textBox1.BackColor = colorDialog1.Color;
            }
        }

        private void ShowMenu(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }
    }
}