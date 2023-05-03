using lab2.Factories.SerializerFactories;
using lab2.vehicles;
using lab2.Serializers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using static lab2.MainForm;
using System.Security.AccessControl;

namespace lab2
{
    public partial class MainForm : Form
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        public static Type[] VehiclesTypes = {};

      
        private string filesFilter = string.Empty;
        private List<SerializerFactory> serializerFactories = new List<SerializerFactory>()
        {
            new BinarySerializerFactory(), new XMLSerializerFactory(), new TextSerializerFactory()
        };


        private void HardcodeVehiclesToTable()
        {
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
            vehicles.AddRange(new List<Vehicle> { bus, motorcycle, truck, passengerCar});
        }

        public MainForm()
        {
            InitializeComponent();
            VehiclesTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => 
                t.IsDefined(typeof(DisplayNameAttribute))).ToArray();
            HardcodeVehiclesToTable();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            filesFilter = GetFilesFilter();
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
            }
            else
            {
                editButton.Enabled = true;
                removeButton.Enabled = true;
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

        private string GetFilesFilter()
        {
            var fileFilter = string.Empty;
            foreach (var factory in serializerFactories)
            {   
                fileFilter += factory.GetExtension() + "|";
            }
            return fileFilter.Remove(fileFilter.Length - 1);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = filesFilter;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SerializerFactory? serializerFactory = null;
                foreach (var factory in serializerFactories)
                {
                    if (factory.GetExtension() == saveFileDialog1.Filter)
                    {
                        serializerFactory = factory;
                        break;
                    }
                    
                }
                if (serializerFactory != null)
                {
                    ISerializer serializer = serializerFactory.CreateSerializer();
                    serializer.Serialize(saveFileDialog1.FileName, vehicles);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = filesFilter;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SerializerFactory? serializerFactory = null;
                foreach (var factory in serializerFactories)
                {
                    if (factory.GetExtension() == saveFileDialog1.Filter)
                    {
                        serializerFactory = factory;
                        break;
                    }

                }
                if (serializerFactory != null)
                {
                    ISerializer serializer = serializerFactory.CreateSerializer();
                    try
                    {
                        vehicles = serializer.Deserialize(openFileDialog1.FileName);
                        UpdateTable();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The file is not in the correct format.");
                    }
                }
            }
        }
    }
}