using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
namespace W03_BooksWPF;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Book> loadedBooks = [];
    private List<Book> queriedBooks = [];

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnShowDescription_Click(object sender, RoutedEventArgs e)
    {
        if (listBox.SelectedItem is Book selectedBook)
        {
            MessageBox.Show(selectedBook.Description,
                $"{selectedBook.Author} - {selectedBook.Title} ({selectedBook.Year})", MessageBoxButton.OK,
                MessageBoxImage.None);
        }
        else
        {
            MessageBox.Show("Please select a book from the list.", "No selection", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }

    private void BtnLoad_Click(object sender, RoutedEventArgs e)
    {

        OpenFileDialog dlg = new OpenFileDialog();
        dlg.DefaultExt = ".xml";
        // dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
         dlg.Filter = "Books Files (*.xml;*.json)|*.xml;*.json|XML Files (*.xml)|*.xml|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
        bool? result = dlg.ShowDialog();
        if (result.HasValue)
        {
            string fileName = dlg.FileName;
            loadedBooks = BooksHelper.LoadBooks(fileName);
            queriedBooks = loadedBooks;

            listBox.ItemsSource = queriedBooks;

            var genres = loadedBooks.Select(b => b.Genre).Distinct().ToList();
            comboFilter.ItemsSource = genres;
            comboFilter.SelectedItem = genres.FirstOrDefault();
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.DefaultExt = ".xml";
        // dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
        dlg.Filter = "Books Files (*.xml;*.json)|*.xml;*.json|XML Files (*.xml)|*.xml|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
        bool? result = dlg.ShowDialog();
        if (result.HasValue)
        {
            string fileName = dlg.FileName;
            BooksHelper.SaveBooks(queriedBooks, fileName);
        }
    }

    private void BtnFilter_Click(object sender, RoutedEventArgs e)
    {
        if (comboFilter.SelectedItem is string genre && !string.IsNullOrWhiteSpace(genre))
        {
            queriedBooks = loadedBooks.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            listBox.ItemsSource = queriedBooks;
        }
        else
        {
            queriedBooks = loadedBooks;
            listBox.ItemsSource = queriedBooks;
        }
    }

    private void BtnSearch_Click(object sender, RoutedEventArgs e)
    {
        string query = searchBox.Text.Trim().ToLower();
        // string query = searchBox.Text.ToLower();
        // if (!string.IsNullOrEmpty(query))
        if (!string.IsNullOrWhiteSpace(query))
        {
            queriedBooks = loadedBooks.Where(b => b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query)).ToList();
            // queriedBooks = (from book in loadedBooks where book.Title.ToLower().Contains(query) || book.Author.ToLower().Contains(query) select book).ToList();

            listBox.ItemsSource = queriedBooks;
        }
    }

    private void BtnOrder_Click(object sender, RoutedEventArgs e)
    {

        if (comboOrder.Text == "Ascending")
        {
            queriedBooks = queriedBooks.OrderBy(b => b.Author).ToList();
        }
        else if (comboOrder.Text == "Descending")
        {
            queriedBooks = queriedBooks.OrderByDescending(b => b.Author).ToList();
        }
        listBox.ItemsSource = queriedBooks;
    }
}
