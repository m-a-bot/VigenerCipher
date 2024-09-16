using Microsoft.Win32;
using PVigenerCipher;
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
using static System.Net.Mime.MediaTypeNames;

namespace HackVigenerCipher;

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

    private void SaveEncryptedTextToFile_Click(object sender, RoutedEventArgs e)
    {
        string encryptedText = EncryptedText.Text;

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

    private void SaveHackedTextToFile_Click(object sender, RoutedEventArgs e)
    {
        string hackedText = HackedText.Text;

        if (hackedText == "")
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
            File.WriteAllText(saveFileDialog.FileName, hackedText);
            MessageBox.Show("Текст сохранен в файл");
        }
    }

    private bool AllCharactersOfStringBelongToAlphabet(string line, string alphabet)
    {

        foreach (var character in line)
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

    private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
    {
        EncryptedText.Text = "";

        string alphabet = "";

        if (RusLanguage.IsChecked.Value)
        {
            currentTypeAlphabet = TypeAlphabet.Rus;
            alphabet = rusAlphabet + numbers;
        }
        if (EngLanguage.IsChecked.Value)
        {
            currentTypeAlphabet = TypeAlphabet.Eng;
            alphabet = engAlphabet + numbers;
        }
        VIgenerCipher.Alphabet = alphabet;
        

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

        EncryptedText.Text = VIgenerCipher.Encrypt(source, key);
        MessageBox.Show("Текст зашифрован");
    }

    private void BtnHack_Click(object sender, RoutedEventArgs e)
    {
        HackedText.Text = "";

        string alphabet = "";

        if (RusLanguage.IsChecked.Value)
        {
            currentTypeAlphabet = TypeAlphabet.Rus;
            alphabet = rusAlphabet + numbers;
        }
        if (EngLanguage.IsChecked.Value)
        {
            currentTypeAlphabet = TypeAlphabet.Eng;
            alphabet = engAlphabet + numbers;
        }
        VIgenerCipher.Alphabet = alphabet;


        string encryptedText = EncryptedText.Text;

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

        HackVigener hackVigener = new HackVigener(alphabet, currentTypeAlphabet, encryptedText);

        KeyLength.Text = hackVigener.KeyLength.ToString();

        HackedKey.Text = hackVigener.Key;

        HackedText.Text = hackVigener.Hack();

        MessageBox.Show("Текст дешифрован");
    }
}
