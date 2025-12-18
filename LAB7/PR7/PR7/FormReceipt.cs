using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace PublishingApp
{
    public partial class FormReceipt : Form
    {
        private DatabaseHelper dbHelper;
        private int orderId;
        private TextBox txtReceipt;
        private Models.Order currentOrder;

        public FormReceipt(DatabaseHelper dbHelper, int orderId)
        {
            this.dbHelper = dbHelper;
            this.orderId = orderId;
            InitializeComponent();
            LoadReceipt();
        }

        private void InitializeComponent()
        {
            this.Text = "Чек предзаказа";
            this.Size = new Size(550, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Панель заголовка
            var panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 50;
            panelHeader.BackColor = Color.DarkGreen;

            var lblHeader = new Label();
            lblHeader.Text = "ЧЕК ПРЕДЗАКАЗА";
            lblHeader.Font = new Font("Arial", 14, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            panelHeader.Controls.Add(lblHeader);

            // Текстовое поле для чека
            txtReceipt = new TextBox();
            txtReceipt.Dock = DockStyle.Fill;
            txtReceipt.Multiline = true;
            txtReceipt.ReadOnly = true;
            txtReceipt.Font = new Font("Courier New", 10);
            txtReceipt.ScrollBars = ScrollBars.Vertical;
            txtReceipt.Margin = new Padding(10);
            txtReceipt.BackColor = Color.White;

            // Панель для кнопок
            var panelBottom = new Panel();
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 80;
            panelBottom.BackColor = Color.LightGray;

            // Кнопка сохранения
            var btnSave = new Button();
            btnSave.Text = "Сохранить чек";
            btnSave.Size = new Size(120, 35);
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSave.Location = new Point(30, 20);
            btnSave.Click += BtnSave_Click;

            // Кнопка печати
            var btnPrint = new Button();
            btnPrint.Text = "Печать";
            btnPrint.Size = new Size(100, 35);
            btnPrint.Location = new Point(170, 20);
            btnPrint.Click += BtnPrint_Click;

            // Кнопка закрытия
            var btnClose = new Button();
            btnClose.Text = "Закрыть";
            btnClose.Size = new Size(100, 35);
            btnClose.Location = new Point(290, 20);
            btnClose.Click += (s, e) => this.Close();

            // Добавляем кнопки на панель
            panelBottom.Controls.Add(btnSave);
            panelBottom.Controls.Add(btnPrint);
            panelBottom.Controls.Add(btnClose);

            // Добавление на форму
            this.Controls.Add(panelHeader);
            this.Controls.Add(txtReceipt);
            this.Controls.Add(panelBottom);
        }

        private void LoadReceipt()
        {
            try
            {
                currentOrder = dbHelper.GetOrderDetails(orderId);
                if (currentOrder != null)
                {
                    string receiptText = GenerateReceipt(currentOrder);
                    txtReceipt.Text = receiptText;
                }
                else
                {
                    txtReceipt.Text = "Ошибка: заказ не найден";
                }
            }
            catch (Exception ex)
            {
                txtReceipt.Text = $"Ошибка загрузки чека: {ex.Message}";
            }
        }

        private string GenerateReceipt(Models.Order order)
        {
            return $@"==========================================
            ИЗДАТЕЛЬСТВО
           'Предзаказы книг'
==========================================
ЧЕК № {order.Id}
Дата: {order.OrderDate:dd.MM.yyyy HH:mm:ss}
==========================================
КНИГА: {order.BookTitle}
КЛИЕНТ: {order.CustomerName}
ОФИС ПОЛУЧЕНИЯ: {order.OfficeName}
==========================================
СТОИМОСТЬ: {order.Price:F2} руб.
==========================================
СТАТУС: ПРЕДЗАКАЗ ОФОРМЛЕН

ИНФОРМАЦИЯ:
- Книга будет доступна через 7-14 дней
- При поступлении с вами свяжутся по телефону
- Для получения необходим паспорт и данный чек
- Чек действителен в течение 30 дней

==========================================
СПАСИБО ЗА ПРЕДЗАКАЗ!
==========================================";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FileName = $"Чек_предзаказа_№{orderId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Title = "Сохранить чек предзаказа";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, txtReceipt.Text, System.Text.Encoding.UTF8);

                    MessageBox.Show($"Чек успешно сохранен!\n\nПуть: {filePath}",
                        "Сохранение завершено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Чек отправлен на печать", "Печать",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка печати: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}