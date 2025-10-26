namespace DZ_S1
{
    using System.Collections;
    using System.Text;

    public partial class Form1 : Form
    {
        private DrugList Storage;
        private DrugList Bill;
        private Logger Debug;
        public Form1()
        {
            InitializeComponent();
            Storage = new DrugList();
            Bill = new DrugList();
            Debug = new Logger();
        }
        //Загрузка бази даних
        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            Debug.Log("Load database...");
            CryptoGamma G = new CryptoGamma();
            BinaryReader? R = new BinaryReader(File.OpenRead("database"));
            if (R == null)
            {
                Debug.Log("Load database error!");
                MessageBox.Show("Can't load database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Storage.Clear();
            //Читаємо базу
            byte[] b = R.ReadBytes((int)R.BaseStream.Length);
            //Дешифрування
            for (int i = 0; i < b.Length; i++)
            {
                b[i] ^= G.Get();
            }
            //Ітеруємо усі записи
            for (int i = 0; i < b.Length;)
            {
                Drug d;
                d = new Drug(b, ref i);
                if (i < 0)
                {
                    Debug.Log("Database corrupted????");
                    break;
                }
                Debug.Log(d + " loaded!");
                Storage.Add(d, 0);
            }
            Debug.Log("Database loaded!");
            R.Close();
            Storage.Draw(lvStorage);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        //Додати на склад
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name;
            UInt64 sc;
            double d_price;
            //Тестуємо валідність даних
            name = tbName.Text;
            if (name.Length < 3)
            {
                MessageBox.Show("Incorrect input <" + name + ">, too short!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!UInt64.TryParse(tbScancode.Text, out sc))
            {
                MessageBox.Show("Incorrect input <" + tbScancode.Text + ">, only digits allowed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(tbPrice.Text, out d_price) || d_price <= 0.01)
            {
                MessageBox.Show("Incorrect input <" + tbPrice.Text + ">, only positive number allowed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Перераховуєм у формат с фіксованою десятковою комою
            UInt32 price = (UInt32)Math.Round(d_price * 100.0);
            //Додаємо таблетку до складу
            Drug d;
            d = new Drug(name, sc, price);
            Debug.Log(d + " added to storage");
            Storage.Add(d, 1);
            //Оновлюємо зображення склада
            Storage.Draw(lvStorage);
        }
        //Додати до замовлення
        private void btnSell_Click(object sender, EventArgs e)
        {
            int i;
            if (lvStorage.SelectedIndices.Count != 1) return;
            i = lvStorage.SelectedIndices[0];
            Drug? d = Storage.Reserve(i);
            if (d == null) return;
            Debug.Log(d + " added to bill");
            Bill.Add(d, 1);
            Bill.Draw(lvBill);
            Storage.Draw(lvStorage);
            lvStorage.Focus();
            lvStorage.Items[i].Selected = true;
        }
        //Видача замовлення
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Debug.Log("Checkout");
            Bill.Clear();
            Bill.Draw(lvBill);
        }
        //Зберігання бази даних у файл
        private void btnSaveDB_Click(object sender, EventArgs e)
        {
            Debug.Log("Save database...");
            CryptoGamma G = new CryptoGamma();
            BinaryWriter? W = new BinaryWriter(File.OpenWrite("database"));
            if (W == null)
            {
                Debug.Log("Can't store database!");
                MessageBox.Show("Can't store database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Storage.WriteAllToFile(W, G) == 0)
            {
                Debug.Log("Database saved!");
                W.Flush();
                W.Close();
                MessageBox.Show("Database saved!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Debug.Log("Can't store database (2)!");
                W.Flush();
                W.Close();
                MessageBox.Show("Can't store database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Повертаємо усі таблетки з чеку на склад
        private void btnResetBill_Click(object sender, EventArgs e)
        {
            Debug.Log("Reseting bill...");
            for(Drug? d; (d=Bill.Remove(0))!=null;)
            {
                Debug.Log(d + " revert to storage");
                Storage.Add(d, 0);
            }
            Bill.Draw(lvBill);
            Storage.Draw(lvStorage);
            Debug.Log("Reseting bill complete");
        }
    }
    //Клас таблетки
    public class Drug
    {
        //Дані таблетки - сканкод, ціна, назва, кількість
        private UInt64 scancode;
        private UInt32 price;
        private string name;
        private UInt32 count;
        //Конструктор зі змінних
        public Drug(string _name, UInt64 _scancode, UInt32 _price)
        {
            name = _name;
            scancode = _scancode;
            price = _price;
            count = 0;
        }
        //Конструктор з бінарного вигляду
        public Drug(byte[] b, ref int i)
        {
            name = "unknown";
            int sz = b.Length;
            int nl;
            if (i > sz - 8) { i = -1; return; }
            scancode = BitConverter.ToUInt64(b, i);
            i += 8;
            if (i > sz - 4) { i = -1; return; }
            price = BitConverter.ToUInt32(b, i);
            i += 4;
            if (i > sz - 4) { i = -1; return; }
            count = BitConverter.ToUInt32(b, i);
            i += 4;
            if (i > sz - 4) { i = -1; return; }
            nl = BitConverter.ToInt32(b, i);
            i += 4;
            nl *= 2;
            if (i > sz - nl) { i = -1; return; }
            name = Encoding.Unicode.GetString(b, i, nl);
            i += nl;
        }
        //Отримати сканкод
        public UInt64 GetScancode()
        {
            return scancode;
        }
        //Перевірити, чи співпадають сканкоди таблеток
        public bool TestScancode(Drug? d)
        {
            if (d == null) return false;
            return scancode == d.scancode;
        }
        //Збільшити кількість
        public void AddN(UInt32 N)
        {
            count += N;
        }
        //Зменшити кількість
        public void SubN(UInt32 N)
        {
            if (N <= count)
                count -= N;
        }
        //Конверсія ціни в строку
        public string getPriceAsStr()
        {
            return String.Format("{0}.{1,2:D2}",price/100,price%100);
        }
        //Конверсія всієї інформації в строку
        public override string ToString()
        {
            return String.Format(
                "('{0}',SC={1},P={2},N={3})",
                name, scancode, getPriceAsStr(), count);
        }
        //Отримати кількість
        public UInt32 GetCount()
        {
            return count;
        }
        //Генеруємо ListViewItem по даним таблетки
        public ListViewItem ToLVI()
        {
            string[] a =
            {
                name,
                scancode.ToString(),
                getPriceAsStr(),
                count.ToString()
            };
            ListViewItem item = new ListViewItem(a);
            return item;
        }
        //Робимо копію таблетки
        public Drug Clone0()
        {
            Drug d = new Drug(name, scancode, price);
            return d;
        }
        //Робимо копію таблетки з кількістю
        public Drug CloneWithCount()
        {
            Drug d = new Drug(name, scancode, price);
            d.count = count;
            return d;
        }
        //Конвертація в бінарний вигляд для збереження на диск
        public byte[] ToBinary()
        {
            List<byte> b = new List<byte>();
            b.AddRange(BitConverter.GetBytes(scancode));
            b.AddRange(BitConverter.GetBytes(price));
            b.AddRange(BitConverter.GetBytes(count));
            b.AddRange(BitConverter.GetBytes((int)(name.Length)));
            b.AddRange(Encoding.Unicode.GetBytes(name));
            return b.ToArray();
        }
    }
    //Клас списку таблеток
    public class DrugList
    {
        //Список таблеток
        private List<Drug> list;
        //Конструктор
        public DrugList()
        {
            list = new List<Drug>();
        }
        //Видаляємо усі таблетки
        public void Clear()
        {
            list.Clear();
        }
        //Додаєемо нову таблетку або оновлюємо кількість
        public int Add(Drug? d, UInt32 N)
        {
            if (d == null) return 0;
            //Шукаємо таблетку з таким сканкодом
            Drug? ed = list.Find(x => x.TestScancode(d));
            if (ed == null)
            {
                //Не знайшли таку таблетку, додаємо
                list.Add(d);
                ed = d;
            }
            else
                N += d.GetCount();
            //Оновлюємо кількість
            ed.AddN(N);
            return 0;
        }
        //Резервуємо одну таблету для переміщення в інший список
        public Drug? Reserve(int index)
        {
            if (index >= list.Count) return null;
            if (list[index].GetCount() < 1) return null;
            list[index].SubN(1);
            return list[index].Clone0();
        }
        //Видаляємо таблетку
        public Drug? Remove(int index)
        {
            if (index >= list.Count) return null;
            Drug d = list[index].CloneWithCount();
            list.RemoveAt(index);
            return d;
        }
        //Отримуємо кількість таблеток по індексу
        public UInt32 GetCount(int index)
        {
            if (index >= list.Count) return 0;
            return list[index].GetCount();
        }
        //Оновлення даних для відображеня
        public void Draw(ListView dest)
        {
            //Видаляємо рядкі, якіх вже немає
            while (dest.Items.Count > list.Count)
            {
                dest.Items.RemoveAt(dest.Items.Count - 1);
            }
            //Оновлюємо існуючи або додаємо нові рядки
            for (int i = 0; i < list.Count; i++)
            {
                if (i < dest.Items.Count)
                    //Оновлюємо рядок
                    dest.Items[i] = list[i].ToLVI();
                else
                    //Додаємо рядок
                    dest.Items.Add(list[i].ToLVI());
            }
        }
        //Зберегти склад в файл
        public int WriteAllToFile(BinaryWriter W, CryptoGamma G)
        {
            for (int i = 0; i < list.Count; i++)
            {
                byte[] ba = list[i].ToBinary();
                for (int j = 0; j < ba.Length; j++)
                {
                    ba[j] ^= G.Get();
                }
                W.Write(ba);
            }
            return 0;
        }
    }
    //Клас для генерації гами
    public class CryptoGamma
    {
        private UInt64 seed;
        public CryptoGamma()
        {
            seed = 1234567; //Початкове значення
        }
        //Отримати наступне значення гами
        public byte Get()
        {
            seed = seed * 6364136223846793005 + 1442695040888963407;
            return (byte)(seed & 0xFF);
        }
    }
    //Клас лог-файла
    public class Logger
    {
        //Додати строку до лог-файла з датою та часом
        public void Log(string s)
        {
            File.AppendAllText("drugstore.log",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") 
                + s
                + "\n");
        }
    }
}
