/*
using System;
using bangazon_cli.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace bangazon_cli.Menus
{
    /*
        Author: Kimberly Bird
        Summary - controls the active customer report
        Parameters:
            - Customer object (Active Customer Id)
            - Product object 
            - Order object
            - OrderProduct object
            - PaymentType object
    */
    /*
     

    public class ViewActiveCustomerRevReport
    {
        private DatabaseInterface _db;
        private Customer _customer;
        private Order _order;
        private Product _product;
        private PaymentType _paymentType;

        public ViewReportMenu(Customer customer, Order order, Product product, PaymentType paymentType)
        {
            _customer = customer;
            _order = order;
            _product = product;
            _paymentType = paymentType;
        }

        // Displays view report menu to user
        public void Show()
        {
            Console.Clear();
            // missing option to exit report
            int ouput = 0;
            do 
            {
                _db.Query($@"SELECT 
                    c.Name as 'Customer Name', 
                    o.Id as 'Order Number', 
                    p.Name as 'Product Name', 
                    p.Quantity as 'Quantity',
                    p.price as 'Price',
                    SUM(p.Quantity * p.Price) as 'Total Revenue Per Product' ,
                    pt.Type as 'Payment Type'
                FROM 
                    Customer c,
                    Order o,
                    Product p,
                    PaymentType pt,
                    OrderProduct op
                where c.Id = p.CustomerId
                and p.Id = op.ProductId
                and c.Id = o.CustomerId
                and o.id = op.OrderId
                group by p.Name
                order by o.Id;", 
                (SqliteDataReader reader) =>
                {
                    customer.Name = Convert.ToString(reader["Customer Name"]);
                    order.Id = Convert.ToInt32(reader["Order Number"]);
                    product.Name = Convert.ToString(reader["Product"]);
                    product.Quantity = Convert.ToInt32(reader["Quantity"]);
                    // missing SUM
                    paymentType.Type = Convert.ToString(reader["Payment Type"]);
                });
                // output data
                Console.WriteLine();
                
            } while (ouput < 1);
        }
    }


}
*/


