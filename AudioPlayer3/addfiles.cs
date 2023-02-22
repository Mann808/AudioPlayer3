using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace AudioPlayer3
{
    static class addfiles
    {
        static public string startPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

        static public void AddMusicCollection()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.InitialDirectory = startPath;
                openFileDialog.Filter = "Аудио файлы (*.mp3)|*.mp3";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string files in openFileDialog.FileNames)
                    {
                        music.AddMusic(files);
                    }
                }
            }
        }
    }
}
