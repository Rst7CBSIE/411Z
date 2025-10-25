namespace DZ_S1
{
    using System.Collections;
    public partial class Form1 : Form
    {
        private DrugList Storage;
        public Form1()
        {
            InitializeComponent();
            Storage = new DrugList();
        }

        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            Storage.Clear();
            Drug d;
            d = new Drug("Ibuprofen", 12345678, 1488);
            Storage.Add(d,5);
            Storage.Draw(lvStorage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
        public bool TestScancode(Drug d)
        {
            return scancode == d.scancode;
        }
        public void AddN(UInt32 N)
        {
            count += N;
        }
        public string getPriceAsStr()
        {
            return String.Format("{0}.{1,2}",price/100,price%100);
        }
        public override string ToString()
        {
            return String.Format(
                "('{0}',{1},{2},{3})",
                name, scancode, getPriceAsStr(), count);
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
            if (ed != null)
            {
            }
            else
            {
                list.Add(d);
                ed = d;
            }
            ed.AddN(N);
            return 0;
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
