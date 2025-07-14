using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Db3Project20Entities db = new Db3Project20Entities();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Toplam kategori sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            //Toplam ürün sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();

            //Toplam müşteri sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();

            //Toplam sipariş sayısı
            int ordercount = db.TblOrder.Count();
            lblOrderCount.Text = ordercount.ToString();


            //Toplam Stok Sayısı

            var totalProductStockCount= db.TblProduct.Sum(x => x.ProductStock);
            lblProductTotalStock.Text = totalProductStockCount.ToString();

            //Ortalama Ürün Fiyatı

            var averageProductPrice=db.TblProduct.Average(x => x.ProductPrice);
            lblProductAveragePrice.Text = averageProductPrice.ToString()+ " ₺";


            // Toplam meyve stoğu
            var totalProductCountByCategoryIsFruit=db.TblProduct
             .Where(x=>x.CategoryId == 1).Sum(y=>y.ProductStock);
            lblProductCountByCategoryIsFruit.Text = totalProductCountByCategoryIsFruit.ToString();

            //Gazoz işlem hacmi
            var totalPriceByProductNameIsGazozGetStock = db.TblProduct
                .Where(x => x.ProductName == "Gazoz").Select(y => y.ProductStock).FirstOrDefault();
                
            var totalPriceByProductNameIsGazozGetUniitPrice = db.TblProduct
                .Where(x => x.ProductName == "Gazoz").Select(y => y.ProductPrice).FirstOrDefault();

            var totalPriceByProductNameIsGazoz = totalPriceByProductNameIsGazozGetStock *
                totalPriceByProductNameIsGazozGetUniitPrice;
            lblTotalPriceByProductNameIsGazoz.Text = totalPriceByProductNameIsGazoz.ToString() + " ₺";

            //Stoğu 100 den az ürünler

            var productCountByStockCountSmallerThan100=db.TblProduct
                .Where(x => x.ProductStock < 100).Count();
            lblProductStockSmallerThan100.Text=productCountByStockCountSmallerThan100.ToString();

            //Aktif Sebze Stoğu 

            var productStockCountByCategoryNameIsSebzeAndStatusIsTrue= db.TblProduct.Where(x=>x.CategoryId==(2) && x.ProductStatus == true)
                .Sum(y => y.ProductStock);
            lblProgramCountByCategorySebzeAndStatusTrue.Text = productStockCountByCategoryNameIsSebzeAndStatusIsTrue.ToString();

            //Turkiye'den yapılan siparişler SQL style
            var orderCountFromTurkiye= db.Database.SqlQuery<int>
            ("Select count (*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where CustomerCountry='Türkiye' )"). FirstOrDefault();
            lblOrderCountFromTurkiyeBySQL.Text = orderCountFromTurkiye.ToString();

            //Türkiye'den yapılan siparişler EF style 

            var turkishCustomerIds= db.TblCustomer. Where(x=>x.CustomerCountry == "Türkiye").
                Select(y=>y.CustomerId).ToList();

            var orderCountFromTurkiyeWithEf=db.TblOrder.Count(z=>turkishCustomerIds.Contains(z.CustomerId.Value));
            lblOrderCountFromTurkiyeByEf.Text=orderCountFromTurkiyeWithEf.ToString();


            //Toplam Meyve Satış Kazancı SQL Sorgusu

                  var orderTotalPriceByCategoryIsMeyve = db.Database.SqlQuery<decimal>
            ("Select Sum(o.TotalPrice) From TblOrder o Join TblProduct p On o.ProductId=p.ProductId Join TblCategory c On p.CategoryId=c.CategoryId Where c. CategoryName='Meyve'").FirstOrDefault();
            lblOrderTotalPriceByCategoryIsMeyve.Text = orderTotalPriceByCategoryIsMeyve.ToString() + " ₺";

            //Toplam meyve satış kazancı Ef Metodu  style

            var orderTotalPriceByCategoryIsMeyveEf = (from o in db.TblOrder
                                                      join p in db.TblProduct on o.ProductId equals p.ProductId
                                                      join c in db.TblCategory on p.CategoryId equals c.CategoryId
                                                      where c.CategoryName == "Meyve"
                                                      select o.TotalPrice).Sum();
            lblOrderTotalPriceByCategoryIsMeyveEf.Text=orderTotalPriceByCategoryIsMeyveEf.ToString() + " ₺";

            //Son Eklenen Ürün Adı
            var lastProductName = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductName).FirstOrDefault();
            lblLastProductName.Text = lastProductName.ToString();


            //Son Eklenen Ürün  Kategori Adı
            var lastProductCategoryId = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y=>y.CategoryId).FirstOrDefault();
            var lastProductCategoryName = db.TblCategory.Where(x => x.CategoryId == lastProductCategoryId).Select(y => y.CategoryName).FirstOrDefault();
            lblLastProductCategoryName.Text = lastProductCategoryName.ToString();

            //Aktif Ürün Sayısı

            var activeProductCount = db.TblProduct.Where(x => x.ProductStatus == true).Count();
            lblActiveProductCount.Text = activeProductCount.ToString();


            //Toplam Kola Stok SatışlarındN Kazanılan Para 
            var colaStock= db.TblProduct . Where (x => x.ProductName == "Kola"). Select (y=>y.ProductStock).FirstOrDefault();
            var colaPrice = db.TblProduct.Where(x=>x.ProductName=="Kola").Select(y => y.ProductPrice)
                .FirstOrDefault();
            var totalColaStockPrice = colaStock * colaPrice;
            lblTotalPriceWithStockByCola.Text = totalColaStockPrice.ToString() + " ₺";


            //Son Sipariş Veren Müşteri Adı

            var lastCustomerId= db.TblOrder.OrderByDescending(x => x.OrderId).Select(y => y.CustomerId).FirstOrDefault();
            var lastCustomerName = db.TblCustomer.Where(x => x.CustomerId==lastCustomerId).Select(y=>y.CustomerName).FirstOrDefault();          
            lblLastCustomerName.Text = lastCustomerName.ToString();


            //Ülke Çeşitliliği Sayısı 

            var countryDifferentCount= db.TblCustomer.Select(x=>x.CustomerCountry ).Distinct().Count();
            lblCountryDifferentCount.Text = countryDifferentCount.ToString();

        }



    }
}
