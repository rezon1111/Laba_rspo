using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace PublishingApp
{
    public partial class FormOrder : Form
    {
        private DatabaseHelper dbHelper;
        private Models.Book selectedBook;
        private ComboBox comboBoxOffices;
        private TextBox txtCustomerName, txtCustomerAddress, txtCustomerPhone;
        private NumericUpDown numQuantity;
        private Label lblTotalPrice;
        private Label lblBookInfo;
        private Button btnConfirm;
        private Button btnCancel;

        public FormOrder(DatabaseHelper dbHelper, Models.Book book)
        {
            this.dbHelper = dbHelper;
            this.selectedBook = book;
            InitializeComponent();
            LoadOffices();
            CalculatePrice();
        }

        private void InitializeComponent()
        {
            this.Text = "Оформление предзаказа";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Панель заголовка
            var panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 50;
            panelHeader.BackColor = Color.SteelBlue;

            var lblHeader = new Label();
            lblHeader.Text = "ОФОРМЛЕНИЕ ПРЕДЗАКАЗА";
            lblHeader.Font = new Font("Arial", 12, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            panelHeader.Controls.Add(lblHeader);

            // Основной контейнер
            var panelMain = new Panel();
            panelMain.Dock = DockStyle.Fill;
            panelMain.Padding = new Padding(20);
            panelMain.AutoScroll = true;

            int yPos = 20;

            // Информация о книге
            var lblBookTitle = new Label();
            lblBookTitle.Text = "Выбранная книга:";
            lblBookTitle.AutoSize = true;
            lblBookTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblBookTitle.Location = new Point(20, yPos);
            yPos += 25;

            lblBookInfo = new Label();
            lblBookInfo.AutoSize = false;
            lblBookInfo.Size = new Size(400, 60);
            lblBookInfo.Location = new Point(20, yPos);
            lblBookInfo.Font = new Font("Arial", 9);
            lblBookInfo.BorderStyle = BorderStyle.FixedSingle;
            lblBookInfo.Padding = new Padding(5);
            yPos += 70;

            // Количество
            var lblQuantity = new Label();
            lblQuantity.Text = "Количество экземпляров:";
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(20, yPos);
            yPos += 25;

            numQuantity = new NumericUpDown();
            numQuantity.Location = new Point(20, yPos);
            numQuantity.Size = new Size(100, 25);
            numQuantity.Minimum = 1;
            numQuantity.Maximum = 10;
            numQuantity.Value = 1;
            numQuantity.ValueChanged += (s, e) => CalculatePrice();
            yPos += 40;

            // Поля ввода данных клиента
            var lblCustomerTitle = new Label();
            lblCustomerTitle.Text = "Данные клиента:";
            lblCustomerTitle.AutoSize = true;
            lblCustomerTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblCustomerTitle.Location = new Point(20, yPos);
            yPos += 25;

            var lblCustomerName = new Label();
            lblCustomerName.Text = "ФИО клиента:*";
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(20, yPos);
            yPos += 25;

            txtCustomerName = new TextBox();
            txtCustomerName.Size = new Size(400, 25);
            txtCustomerName.Location = new Point(20, yPos);
            txtCustomerName.MaxLength = 100;
            yPos += 40;

            var lblCustomerAddress = new Label();
            lblCustomerAddress.Text = "Адрес доставки:";
            lblCustomerAddress.AutoSize = true;
            lblCustomerAddress.Location = new Point(20, yPos);
            yPos += 25;

            txtCustomerAddress = new TextBox();
            txtCustomerAddress.Size = new Size(400, 25);
            txtCustomerAddress.Location = new Point(20, yPos);
            txtCustomerAddress.MaxLength = 200;
            yPos += 40;

            var lblCustomerPhone = new Label();
            lblCustomerPhone.Text = "Телефон:*";
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Location = new Point(20, yPos);
            yPos += 25;

            txtCustomerPhone = new TextBox();
            txtCustomerPhone.Size = new Size(400, 25);
            txtCustomerPhone.Location = new Point(20, yPos);
            txtCustomerPhone.MaxLength = 20;
            yPos += 40;

            // Выбор офиса
            var lblOffice = new Label();
            lblOffice.Text = "Офис получения:*";
            lblOffice.AutoSize = true;
            lblOffice.Location = new Point(20, yPos);
            yPos += 25;

            comboBoxOffices = new ComboBox();
            comboBoxOffices.Size = new Size(400, 25);
            comboBoxOffices.Location = new Point(20, yPos);
            comboBoxOffices.DropDownStyle = ComboBoxStyle.DropDownList;
            yPos += 40;

            // Стоимость
            var lblPrice = new Label();
            lblPrice.Text = "Стоимость заказа:";
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Arial", 10, FontStyle.Bold);
            lblPrice.Location = new Point(20, yPos);
            yPos += 25;

            lblTotalPrice = new Label();
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTotalPrice.ForeColor = Color.Green;
            lblTotalPrice.Location = new Point(180, yPos - 25);
            yPos += 40;

            // Кнопки
            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Size = new Size(120, 35);
            btnCancel.Location = new Point(250, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            btnConfirm = new Button();
            btnConfirm.Text = "Оформить заказ";
            btnConfirm.Size = new Size(150, 35);
            btnConfirm.Location = new Point(80, yPos);
            btnConfirm.BackColor = Color.ForestGreen;
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Font = new Font("Arial", 10, FontStyle.Bold);
            btnConfirm.Click += BtnConfirm_Click;

            // Добавление элементов на панель
            panelMain.Controls.Add(lblBookTitle);
            panelMain.Controls.Add(lblBookInfo);
            panelMain.Controls.Add(lblQuantity);
            panelMain.Controls.Add(numQuantity);
            panelMain.Controls.Add(lblCustomerTitle);
            panelMain.Controls.Add(lblCustomerName);
            panelMain.Controls.Add(txtCustomerName);
            panelMain.Controls.Add(lblCustomerAddress);
            panelMain.Controls.Add(txtCustomerAddress);
            panelMain.Controls.Add(lblCustomerPhone);
            panelMain.Controls.Add(txtCustomerPhone);
            panelMain.Controls.Add(lblOffice);
            panelMain.Controls.Add(comboBoxOffices);
            panelMain.Controls.Add(lblPrice);
            panelMain.Controls.Add(lblTotalPrice);
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(btnConfirm);

            // Добавление на форму
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelMain);
        }

        private void LoadOffices()
        {
            try
            {
                var offices = dbHelper.GetOffices();
                comboBoxOffices.DataSource = offices;
                comboBoxOffices.DisplayMember = "Name";
                comboBoxOffices.ValueMember = "Id";

                if (offices.Count > 0)
                    comboBoxOffices.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки офисов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBookInfo()
        {
            lblBookInfo.Text = $"Название: {selectedBook.Title}\n" +
                             $"Автор: {selectedBook.AuthorName}\n" +
                             $"Год: {selectedBook.ReleaseYear}, Страниц: {selectedBook.Pages}\n" +
                             $"Тираж: {selectedBook.Circulation:N0} экз.";
        }

        private void CalculatePrice()
        {
            // Базовая цена (например, 500 руб за книгу)
            decimal basePrice = 500m;
            // Учет тиража (чем меньше тираж, тем дороже)
            decimal circulationFactor = selectedBook.Circulation > 10000 ? 1.0m : 1.5m;
            // Учет количества страниц
            decimal pageFactor = selectedBook.Pages > 500 ? 1.2m : 1.0m;

            int quantity = (int)numQuantity.Value;
            decimal totalPrice = basePrice * circulationFactor * pageFactor * quantity;

            lblTotalPrice.Text = $"{totalPrice:F2} руб.";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
                    throw new Exception("Введите ФИО клиента");

                if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text))
                    throw new Exception("Введите телефон клиента");

                if (comboBoxOffices.SelectedItem == null)
                    throw new Exception("Выберите офис получения");

                // Создание клиента
                var customer = new Models.Customer
                {
                    Name = txtCustomerName.Text,
                    Address = txtCustomerAddress.Text,
                    Phone = txtCustomerPhone.Text
                };

                int customerId = dbHelper.CreateCustomer(customer);

                // Создание заказа
                int quantity = (int)numQuantity.Value;
                decimal totalPrice = decimal.Parse(lblTotalPrice.Text.Replace(" руб.", ""));

                var order = new Models.Order
                {
                    OrderName = $"Предзаказ: {selectedBook.Title} ({quantity} шт.)",
                    BookId = selectedBook.Id,
                    CustomerId = customerId,
                    OfficeId = (int)comboBoxOffices.SelectedValue,
                    OrderDate = DateTime.Now,
                    Price = totalPrice
                };

                int orderId = dbHelper.CreateOrder(order);

                // Показать чек
                using (var formReceipt = new FormReceipt(dbHelper, orderId))
                {
                    formReceipt.ShowDialog();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка оформления заказа: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateBookInfo();
            CalculatePrice();
        }
    }
}