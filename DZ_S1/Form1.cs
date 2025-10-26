namespace DZ_S1
{
    using System.Collections;
    public partial class Form1 : Form
    {
        private DrugList Storage;
        private DrugList Bill;
        public Form1()
        {
            InitializeComponent();
            Storage = new DrugList();
            Bill = new DrugList();
        }

        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            Storage.Clear();
            Drug d;
            d = new Drug("Ibuprofen", 12345678, 1488);
            Storage.Add(d, 5);
            Storage.Draw(lvStorage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name;
            UInt64 sc;
            double d_price;
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
            UInt32 price = (UInt32)Math.Round(d_price * 100.0);
            Drug d;
            d = new Drug(name, sc, price);
            Storage.Add(d, 1);
            Storage.Draw(lvStorage);
        }
        private void btnSell_Click(object sender, EventArgs e)
        {
            int i;
            if (lvStorage.SelectedIndices.Count != 1) return;
            i = lvStorage.SelectedIndices[0];
            Drug d = Storage.Reserve(i);
            if (d == null) return;
            Bill.Add(d, 1);
            Bill.Draw(lvBill);
            Storage.Draw(lvStorage);
            lvStorage.Focus();
            lvStorage.Items[i].Selected = true;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Bill.Clear();
            Bill.Draw(lvBill);
        }
    }
    public class Drug
    {
        private UInt64 scancode;
        private UInt32 price;
        private string name;
        private UInt32 count;
        public Drug(string _name, UInt64 _scancode, UInt32 _price)
        {
            name = _name;
            scancode = _scancode;
            price = _price;
            count = 0;
        }
        public UInt64 GetScancode()
        {
            return scancode;
        }
        public bool TestScancode(Drug d)
        {
            return scancode == d.scancode;
        }
        public void AddN(UInt32 N)
        {
            count += N;
        }
        public void SubN(UInt32 N)
        {
            if (N <= count)
                count -= N;
        }
        public string getPriceAsStr()
        {
            return String.Format("{0}.{1,2:D2}",price/100,price%100);
        }
        public override string ToString()
        {
            return String.Format(
                "('{0}',{1},{2},{3})",
                name, scancode, getPriceAsStr(), count);
        }
        public UInt32 GetCount()
        {
            return count;
        }
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
        public Drug Clone()
        {
            Drug d = new Drug(name, scancode, price);
            return d;
        }
    }

    public class DrugList
    {
        private List<Drug> list;
        public DrugList()
        {
            list = new List<Drug>();
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Add(Drug d, UInt32 N)
        {
            Drug ed = list.Find(x => x.TestScancode(d));
            if (ed == null)
            {
                list.Add(d);
                ed = d;
            }
            ed.AddN(N);
            return 0;
        }
        public Drug Reserve(int index)
        {
            if (index >= list.Count) return null;
            if (list[index].GetCount() < 1) return null;
            list[index].SubN(1);
            return list[index].Clone();
        }
        public Drug Remove(int index)
        {
            if (index >= list.Count) return null;
            Drug d = list[index].Clone();
            list.RemoveAt(index);
            return d;
        }
        public UInt32 GetCount(int index)
        {
            if (index >= list.Count) return 0;
            return list[index].GetCount();
        }
        public void Draw(ListView dest)
        {
            while (dest.Items.Count>list.Count)
            {
                dest.Items.RemoveAt(dest.Items.Count-1);
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (i < dest.Items.Count)
                    dest.Items[i] = list[i].ToLVI();
                else
                    dest.Items.Add(list[i].ToLVI());
            }
        }

    }
}
