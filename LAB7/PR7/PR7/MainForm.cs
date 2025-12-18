using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace PublishingApp
{
    public partial class MainForm : Form
    {
        private DatabaseHelper dbHelper;
        private DataGridView dataGridViewBooks;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnReset;
        private Button btnOrder;
        private Label lblSelectedInfo;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadBooks();
        }

        private void InitializeComponent()
        {
            this.Text = "Издательство - Оформление предзаказов";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Панель заголовка
            var panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = Color.SteelBlue;

            var lblHeader = new Label();
            lblHeader.Text = "ПРЕДЗАКАЗ КНИГ И ПУБЛИКАЦИЙ";
            lblHeader.Font = new Font("Arial", 18, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            panelHeader.Controls.Add(lblHeader);

            // Панель поиска
            var panelSearch = new Panel();
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Height = 60;
            panelSearch.BackColor = Color.AliceBlue;
            panelSearch.Padding = new Padding(10);

            var lblSearch = new Label();
            lblSearch.Text = "Поиск:";
            lblSearch.Location = new Point(10, 15);
            lblSearch.AutoSize = true;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(60, 12);
            txtSearch.Size = new Size(300, 25);
            txtSearch.PlaceholderText = "Введите название книги или автора...";
            txtSearch.KeyPress += TxtSearch_KeyPress;

            btnSearch = new Button();
            btnSearch.Text = "Найти";
            btnSearch.Location = new Point(370, 10);
            btnSearch.Size = new Size(80, 30);
            btnSearch.BackColor = Color.DodgerBlue;
            btnSearch.ForeColor = Color.White;
            btnSearch.Click += BtnSearch_Click;

            btnReset = new Button();
            btnReset.Text = "Сброс";
            btnReset.Location = new Point(460, 10);
            btnReset.Size = new Size(80, 30);
            btnReset.BackColor = Color.Gray;
            btnReset.ForeColor = Color.White;
            btnReset.Click += BtnReset_Click;

            panelSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch, btnSearch, btnReset });

            // DataGridView для книг
            dataGridViewBooks = new DataGridView();
            dataGridViewBooks.Dock = DockStyle.Fill;
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.MultiSelect = false;
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.SelectionChanged += DataGridViewBooks_SelectionChanged;
            // Важно: подписываемся на событие создания колонок
            dataGridViewBooks.DataBindingComplete += DataGridViewBooks_DataBindingComplete;

            // Панель информации и кнопок
            var panelBottom = new Panel();
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 100;
            panelBottom.BackColor = Color.LightGray;
            panelBottom.Padding = new Padding(10);

            lblSelectedInfo = new Label();
            lblSelectedInfo.Name = "lblSelectedInfo";
            lblSelectedInfo.Text = "Выберите книгу для оформления предзаказа";
            lblSelectedInfo.Location = new Point(10, 15);
            lblSelectedInfo.AutoSize = true;

            btnOrder = new Button();
            btnOrder.Text = "Оформить предзаказ";
            btnOrder.Size = new Size(200, 40);
            btnOrder.Font = new Font("Arial", 11, FontStyle.Bold);
            btnOrder.BackColor = Color.ForestGreen;
            btnOrder.ForeColor = Color.White;
            btnOrder.FlatStyle = FlatStyle.Flat;
            btnOrder.FlatAppearance.BorderSize = 0;
            btnOrder.Location = new Point(650, 30);
            btnOrder.Click += BtnOrder_Click;
            btnOrder.Enabled = false;

            // Кнопка обновления
            var btnRefresh = new Button();
            btnRefresh.Text = "Обновить каталог";
            btnRefresh.Size = new Size(150, 35);
            btnRefresh.Location = new Point(480, 30);
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Click += (s, e) => LoadBooks();

            panelBottom.Controls.AddRange(new Control[] { lblSelectedInfo, btnOrder, btnRefresh });

            // Добавление элементов на форму
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelSearch);
            this.Controls.Add(dataGridViewBooks);
            this.Controls.Add(panelBottom);
        }

        private void InitializeDatabase()
        {
            try
            {
                dbHelper = new DatabaseHelper();
                if (!dbHelper.TestConnection())
                {
                    MessageBox.Show("Подключение к базе данных не установлено.\nПриложение будет работать в ограниченном режиме.",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации базы данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBooks()
        {
            try
            {
                var books = dbHelper.GetBooks();
                dataGridViewBooks.DataSource = books;

                // Не форматируем здесь! Форматирование будет в DataBindingComplete
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Событие вызывается ПОСЛЕ того как все колонки созданы
        private void DataGridViewBooks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FormatDataGridView();
        }

        private void FormatDataGridView()
        {
            // Проверяем, что у нас есть колонки
            if (dataGridViewBooks.Columns == null || dataGridViewBooks.Columns.Count == 0)
                return;

            try
            {
                // Скрываем технические колонки
                if (dataGridViewBooks.Columns.Contains("Id"))
                    dataGridViewBooks.Columns["Id"].Visible = false;

                if (dataGridViewBooks.Columns.Contains("AuthorId"))
                    dataGridViewBooks.Columns["AuthorId"].Visible = false;

                // Устанавливаем заголовки и форматирование
                SetColumnHeader("Title", "Название");
                SetColumnHeader("AuthorName", "Автор");
                SetColumnHeader("ReleaseYear", "Год издания");
                SetColumnHeader("Pages", "Страниц");
                SetColumnHeader("Circulation", "Тираж");

                // Специальное форматирование для ReleaseYear
                if (dataGridViewBooks.Columns.Contains("ReleaseYear"))
                {
                    dataGridViewBooks.Columns["ReleaseYear"].Width = 100;
                    dataGridViewBooks.Columns["ReleaseYear"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Специальное форматирование для Pages
                if (dataGridViewBooks.Columns.Contains("Pages"))
                {
                    dataGridViewBooks.Columns["Pages"].Width = 80;
                    dataGridViewBooks.Columns["Pages"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Специальное форматирование для Circulation
                if (dataGridViewBooks.Columns.Contains("Circulation"))
                {
                    dataGridViewBooks.Columns["Circulation"].Width = 100;
                    dataGridViewBooks.Columns["Circulation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewBooks.Columns["Circulation"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки форматирования
                Console.WriteLine($"Ошибка форматирования DataGridView: {ex.Message}");
            }
        }

        private void SetColumnHeader(string columnName, string headerText)
        {
            if (dataGridViewBooks.Columns.Contains(columnName))
            {
                dataGridViewBooks.Columns[columnName].HeaderText = headerText;
            }
        }

        private void DataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0 && dataGridViewBooks.SelectedRows[0].DataBoundItem is Models.Book book)
            {
                lblSelectedInfo.Text = $"Выбрано: {book.Title} ({book.AuthorName})";
                btnOrder.Enabled = true;
            }
            else
            {
                lblSelectedInfo.Text = "Выберите книгу для оформления предзаказа";
                btnOrder.Enabled = false;
            }
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchBooks();
                e.Handled = true;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchBooks();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadBooks();
        }

        private void SearchBooks()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadBooks();
                return;
            }

            try
            {
                var allBooks = dbHelper.GetBooks();
                var searchText = txtSearch.Text.ToLower();

                var filteredBooks = allBooks.Where(b =>
                    (b.Title != null && b.Title.ToLower().Contains(searchText)) ||
                    (b.AuthorName != null && b.AuthorName.ToLower().Contains(searchText)) ||
                    b.ReleaseYear.ToString().Contains(searchText)
                ).ToList();

                dataGridViewBooks.DataSource = filteredBooks;
                // Форматирование произойдет автоматически в DataBindingComplete

                MessageBox.Show($"Найдено книг: {filteredBooks.Count}", "Результаты поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для предзаказа", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (dataGridViewBooks.SelectedRows[0].DataBoundItem is Models.Book selectedBook)
                {
                    using (var formOrder = new FormOrder(dbHelper, selectedBook))
                    {
                        if (formOrder.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Предзаказ успешно оформлен!\n\nЧек сохранен.",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}