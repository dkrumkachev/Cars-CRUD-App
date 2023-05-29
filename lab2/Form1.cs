using lab2.Factories.SerializerFactories;
using lab2.vehicles;
using lab2.Serializers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using static lab2.MainForm;
using System.Security.AccessControl;
using PluginInterface;
using System.IO;

namespace lab2
{
    public partial class MainForm : Form
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        public static Type[] VehiclesTypes = {};

        private List<string> filesFilter = new();
        private List<SerializerFactory> serializerFactories = new List<SerializerFactory>()
        {
            new BinarySerializerFactory(), new XMLSerializerFactory(), new TextSerializerFactory()
        };
        private Dictionary<string, IEncodingPlugin> plugins = new();


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
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string path = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.FullName + "\\Plugins";
            string[] binDirectories = Directory.GetDirectories(path, "bin", SearchOption.AllDirectories);
            foreach (string binDirectory in binDirectories)
            {
                string[] dllFiles = Directory.GetFiles(binDirectory, "*.dll", SearchOption.AllDirectories);
                foreach (string pluginFile in dllFiles)
                {
                    Assembly pluginAssembly = Assembly.LoadFile(pluginFile);
                    Type[] types = pluginAssembly.GetExportedTypes();
                    foreach (Type type in types)
                    {
                        if (type.GetInterfaces().Contains(typeof(IEncodingPlugin)) && !type.IsAbstract)
                        {
                            object? obj = Activator.CreateInstance(type);
                            if (obj != null)
                            {
                                var plugin = (IEncodingPlugin)obj;
                                plugins.Add(plugin.Extension, plugin);
                            }
                        }
                    }
                }
            }
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

        private SerializerFactory? GetSerializerFactory(FileDialog dialog)
        {
            foreach (var factory in serializerFactories)
            {
                if (factory.GetExtension() == filesFilter[dialog.FilterIndex - 1])
                {
                    return factory;
                }
            }
            return null;
        }

        private List<string> MakeFilesFilter(IEncodingPlugin? plugin)
        {
            if (plugin == null)
            {
                return serializerFactories.Select(i => i.GetExtension()).ToList();
            }
            return serializerFactories.Select(i => i.GetExtension() + plugin.Extension).ToList();
        }

        private List<string> MakeFilesFilter(IEnumerable<IEncodingPlugin> plugins)
        {
            var result = new List<string>();
            foreach (SerializerFactory factory in serializerFactories)
            {
                string filter = factory.GetExtension();
                var extension = filter[filter.LastIndexOf(".")..];
                foreach (IEncodingPlugin plugin in plugins)
                {
                    filter += $";*{extension}{plugin.Extension}";
                }
                result.Add(filter);
            }
            return result;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SelectPlugin(out IEncodingPlugin? plugin))
            {
                return;
            }
            filesFilter = MakeFilesFilter(plugin);
            saveFileDialog1.Filter = string.Join('|', filesFilter);
            saveFileDialog1.FileName = string.Empty;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SerializerFactory serializerFactory = serializerFactories[saveFileDialog1.FilterIndex - 1];
                ISerializer serializer = serializerFactory.CreateSerializer();
                using var memoryStream = new MemoryStream();
                serializer.Serialize(memoryStream, vehicles);
                memoryStream.Position = 0;
                using var fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                if (plugin != null)
                {
                    plugin.Encode(memoryStream, fileStream);
                }
                else
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesFilter = MakeFilesFilter(plugins.Values);
            openFileDialog1.Filter = string.Join('|', filesFilter);
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                SerializerFactory serializerFactory = serializerFactories[openFileDialog1.FilterIndex - 1];
                ISerializer serializer = serializerFactory.CreateSerializer();
                try
                {
                    using var fileStream = new FileStream(path, FileMode.Open);
                    using var memoryStream = new MemoryStream();
                    if (plugins.TryGetValue(Path.GetExtension(path), out IEncodingPlugin? plugin))
                    {
                        plugin.Decode(fileStream, memoryStream);
                    }
                    else
                    {
                        fileStream.CopyTo(memoryStream);
                    }
                    memoryStream.Position = 0;
                    vehicles = serializer.Deserialize(memoryStream);
                    UpdateTable();
                }
                catch (Exception)
                {
                    MessageBox.Show("The file is not in the correct format.");
                }
            }
        }

        private bool SelectPlugin(out IEncodingPlugin? plugin)
        {
            plugin = null;
            var selectionForm = new PluginSelectionForm(plugins.Keys.Select(i => i[1..]).ToList());
            if (selectionForm.ShowDialog() == DialogResult.Cancel)
            {
                return false;
            }
            plugins.TryGetValue($".{selectionForm.Selected}", out plugin);
            return true;
        }
    }
}