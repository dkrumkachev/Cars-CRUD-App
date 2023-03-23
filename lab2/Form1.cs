using lab2.hierarchy;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;


namespace lab2
{
    public partial class MainForm : Form
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        public static Type[] VehiclesTypes = {};

        private void HardcodeVehiclesToTable()
        {
            Bicycle bicycle = new Bicycle();
            bicycle.Manufacturer = "AIST";
            bicycle.Model = "NT-200";
            bicycle.WheelsDiameter = 20;
            bicycle.Driver.Age = 14;
            bicycle.Driver.Name = "Charlie";
            bicycle.Type = Bicycle.BikeTypes.City;
            Motorcycle motorcycle = new Motorcycle();
            motorcycle.Manufacturer = "Yamaha";
            motorcycle.Model = "YFZ";
            motorcycle.Type = Motorcycle.MotorcycleTypes.Sports;
            motorcycle.Engine.FuelType = Engine.FuelTypes.Diesel;
            motorcycle.Engine.Horsepower = 290;
            motorcycle.Driver.Age = 19;
            motorcycle.Driver.Name = "Ann";
            Bus bus = new Bus();
            bus.Manufacturer = "Skoda";
            bus.Model = "706RTO";
            bus.Engine.FuelType = Engine.FuelTypes.Diesel;
            bus.Engine.Horsepower = 130;
            bus.Driver.Age = 31;
            bus.Driver.Name = "John";
            bus.RouteNumber = 127;
            bus.Type = Bus.BusTypes.City;
            Truck truck = new Truck();
            truck.Manufacturer = "Scania";
            truck.Model = "R420";
            truck.Engine.FuelType = Engine.FuelTypes.Gasoline;
            truck.Engine.Horsepower = 200;
            truck.Driver.Age = 54;
            truck.Driver.Name = "Dave";
            truck.MaxWeight = 40;
            truck.HasTrailer = true;
            PassengerCar passengerCar = new PassengerCar();
            passengerCar.Manufacturer = "Tesla";
            passengerCar.Model = "Model S";
            passengerCar.Engine.FuelType = Engine.FuelTypes.Electric;
            passengerCar.Engine.Horsepower = 670;
            passengerCar.Driver.Age = 24;
            passengerCar.Driver.Name = "Alice";
            passengerCar.Drivetrain = PassengerCar.Drivetrains.RearWheel;
            passengerCar.BodyStyle = PassengerCar.BodyStyles.Hatchback;
            passengerCar.HasSeatHeating = true;
            PassengerCar passengerCar1 = new PassengerCar();
            passengerCar1.Manufacturer = "Audi";
            passengerCar1.Model = "A4";
            passengerCar1.Engine.FuelType = Engine.FuelTypes.Gasoline;
            passengerCar1.Engine.Horsepower = 130;
            passengerCar1.Driver.Age = 40;
            passengerCar1.Driver.Name = "Bob";
            passengerCar1.Drivetrain = PassengerCar.Drivetrains.FrontWheel;
            passengerCar1.BodyStyle = PassengerCar.BodyStyles.Sedan;
            passengerCar1.HasSeatHeating = true;
            vehicles.AddRange(new List<Vehicle> { bicycle, bus, motorcycle, passengerCar1, truck, passengerCar});
        }

        public MainForm()
        {
            InitializeComponent();
            VehiclesTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => 
                t.IsDefined(typeof(DisplayNameAttribute))).ToArray();
            HardcodeVehiclesToTable();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void UpdateTable()
        {
            dataGridView.RowCount = vehicles.Count;
            int row = 0;
            foreach (var vehicle in vehicles)
            {
                dataGridView[0, row].Value = row;
                dataGridView[1, row].Value = DisplayNameAttribute.GetDisplayName(vehicle.GetType());
                dataGridView[2, row].Value = vehicle.Manufacturer;
                dataGridView[3, row].Value = vehicle.Model;
                dataGridView[4, row].Value = vehicle.Driver.Name;
                row++;
            }
        }

        private Vehicle GetSelectedItem()
        {
            int index = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            return vehicles[index];
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(AddForm.Mode.Add);
            addForm.ShowDialog();
            if (addForm.DialogResult != DialogResult.Cancel && addForm.Vehicle != null)
            {
                vehicles.Add(addForm.Vehicle);
                UpdateTable();
            }
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(AddForm.Mode.View, GetSelectedItem());
            addForm.ShowDialog();
            UpdateTable();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            int index = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            AddForm addForm = new AddForm(AddForm.Mode.Edit, vehicles[index]);
            addForm.ShowDialog();
            if (addForm.DialogResult != DialogResult.Cancel && addForm.Vehicle != null) 
            {
                vehicles[index] = addForm.Vehicle;
                UpdateTable();
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                editButton.Enabled = false;
                removeButton.Enabled = false;
                viewButton.Enabled = false;
            }
            else
            {
                editButton.Enabled = true;
                removeButton.Enabled = true;
                viewButton.Enabled = true;
            }
        }


        private void removeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this item?", 
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                vehicles.RemoveAt((int)dataGridView.SelectedRows[0].Cells[0].Value);
                UpdateTable();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateTable();
        }
    }
}