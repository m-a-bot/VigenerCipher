using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PVigenerCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TypeAlphabet currentTypeAlphabet;

        string numbers = "0123456789";

        string rusAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        string engAlphabet = "abcdefghijklmnopqrstuvwxyz";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            test1.Text = "";

            changeAlphabet(comboboxAlphabet.SelectedIndex);

            // проверки источника
            // проверка ключа

            string source = SourceText.Text;

            if (source == "")
            {
                MessageBox.Show("Нет текста");
                return;
            }

            if (currentTypeAlphabet == TypeAlphabet.Rus && AnyCharacterOfStringBelongToAlphabet(source, engAlphabet))
            {
                MessageBox.Show("Вводите текст только на русском языке");
                return;
            }
            if (currentTypeAlphabet == TypeAlphabet.Eng && AnyCharacterOfStringBelongToAlphabet(source, rusAlphabet))
            {
                MessageBox.Show("Вводите текст только на английском языке");
                return;
            }

            string key = EncryptionKey.Text.ToLower();

            if (key.Length == 0) 
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            if (!AllCharactersOfStringBelongToAlphabet(key, VIgenerCipher.Alphabet))
            {
                MessageBox.Show("Неправильный ключ");
                return;
            }

            test1.Text = VIgenerCipher.Encrypt(source, key);
            MessageBox.Show("Текст зашифрован");
        }

        private bool AllCharactersOfStringBelongToAlphabet(string line, string alphabet)
        {

            foreach(var character in line)
            {
                if (alphabet.IndexOf(character) == -1)
                    return false;
            }

            return true;
        }

        private bool AnyCharacterOfStringBelongToAlphabet(string line, string alphabet)
        {
            foreach (var character in line)
            {
                if (alphabet.IndexOf(character) != -1)
                    return true;

            }
            return false;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            bool? openFile = openFileDialog.ShowDialog();

            if (openFile.Equals(true))
            {
                SourceText.Text = File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show("Текст загружен из файла");
            }
        }

        private void SaveFile1_Click(object sender, RoutedEventArgs e)
        {
            string encryptedText = test1.Text;

            if (encryptedText == "")
            {
                MessageBox.Show("Нет текста!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;

            bool? saveFile = saveFileDialog.ShowDialog();

            if (saveFile.Equals(true))
            {
                File.WriteAllText(saveFileDialog.FileName, encryptedText);
                MessageBox.Show("Текст сохранен в файл");
            }
        }

        private void SaveFile2_Click(object sender, RoutedEventArgs e)
        {
            string decryptedText = test2.Text;

            if (decryptedText == "")
            {
                MessageBox.Show("Нет текста!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;

            bool? saveFile = saveFileDialog.ShowDialog();

            if (saveFile.Equals(true))
            {
                File.WriteAllText(saveFileDialog.FileName, decryptedText);
                MessageBox.Show("Текст сохранен в файл");
            }
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {

            changeAlphabet(comboboxAlphabet.SelectedIndex);

            test2.Text = "";

            string encryptedText = test1.Text;

            if (encryptedText == "")
            {
                MessageBox.Show("Нет текста");
                return;
            }

            if (currentTypeAlphabet == TypeAlphabet.Rus && AnyCharacterOfStringBelongToAlphabet(encryptedText, engAlphabet))
            {
                MessageBox.Show("Текст должен быть только на русском языке");
                return;
            }
            if (currentTypeAlphabet == TypeAlphabet.Eng && AnyCharacterOfStringBelongToAlphabet(encryptedText, rusAlphabet))
            {
                MessageBox.Show("Текст должен быть только на английском языке");
                return;
            }

            string key = EncryptionKey.Text.ToLower();

            if (key.Length == 0)
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            if (!AllCharactersOfStringBelongToAlphabet(key, VIgenerCipher.Alphabet))
            {
                MessageBox.Show("Неправильный ключ");
                return;
            }

            test2.Text = VIgenerCipher.Decrypt(encryptedText, key);

            MessageBox.Show("Текст расшифрован");
        }

        void changeAlphabet(int index)
        {
            string alphabet = "";

            if (index == 0)
            {
                currentTypeAlphabet = TypeAlphabet.Rus;
                alphabet = rusAlphabet + numbers;
            }
            else if (index == 1)
            {
                currentTypeAlphabet = TypeAlphabet.Eng;
                alphabet = engAlphabet + numbers;
            }
            VIgenerCipher.Alphabet = alphabet;
        }
    }
}
