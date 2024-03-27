using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using up01_01.up01_01DataSetTableAdapters;

namespace up01_01
{
    public class Tables
    {
        public Agency_ServicesTableAdapter Agency_Services = new Agency_ServicesTableAdapter();
        public ClientsTableAdapter Clients = new ClientsTableAdapter();
        public MaterialsTableAdapter Materials = new MaterialsTableAdapter();
        public OfficesTableAdapter Offices = new OfficesTableAdapter();
        public OrdersTableAdapter Orders = new OrdersTableAdapter();
        public ProductsTableAdapter Products = new ProductsTableAdapter();
        public RolesTableAdapter Roles = new RolesTableAdapter();
        public SizesTableAdapter Sizes = new SizesTableAdapter();
        public WaresTableAdapter Wares = new WaresTableAdapter();
        public WorkersTableAdapter Workers = new WorkersTableAdapter();
        public OrdersProductsTableAdapter OrdersProducts = new OrdersProductsTableAdapter();
    }
}
