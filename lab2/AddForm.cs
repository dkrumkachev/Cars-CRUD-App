using lab2.Factories.ControlFactories;
using lab2.Factories.VehicleFactories;
using lab2.hierarchy;
using System.ComponentModel;
using System.Data;
using System.Reflection;


namespace lab2
{
    public partial class AddForm : Form
    {
        private Dictionary<Type, Type> factories = new Dictionary<Type, Type>() {
                { typeof(Bicycle), typeof(BicycleFactory) },
                { typeof(Motorcycle), typeof(MotorcycleFactory) },
                { typeof(PassengerCar), typeof(PassengerCarFactory) },
                { typeof(Bus), typeof(BusFactory) },
                { typeof(Truck), typeof(TruckFactory) }
            };

        private const int ControlWidth = 250;
        private const int ControlHeight = 40;
        private const int Spacing = 20;
        private static readonly Font ControlFont = new("Segoe UI", 14);
        private int prevSelected = -1;
        private string buttonText = string.Empty;
        private ErrorProvider errorProvider;
        public enum Mode { Add, Edit, View };
        private Mode mode;
        public Vehicle? Vehicle { get { return selectedVehicle; } }
        private Vehicle? selectedVehicle;
        public AddForm(Mode mode, Vehicle? vehicle = null)
        {
            InitializeComponent();
            this.mode = mode;
            selectedVehicle = vehicle;
            errorProvider = new ErrorProvider();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            foreach (var type in MainForm.VehiclesTypes)
            {
                var attr = type.GetCustomAttribute<DisplayNameAttribute>();
                if (attr != null)
                {
                    vehicleTypeComboBox.Items.Add(attr.Name);
                }
            }
        }

        private void CenterFormOnScreen()
        {
            Location = new Point((Screen.PrimaryScreen.Bounds.Width - Width) / 2,
                                 (Screen.PrimaryScreen.Bounds.Height - Height) / 2);
        }

        private void AddForm_Shown(object sender, EventArgs e)
        {
            Controls.Clear();
            PlaceControl(this, Spacing, Spacing, ControlWidth, ControlHeight, ControlFont, label1);
            PlaceControl(this, Spacing * 2 + ControlWidth, Spacing, ControlWidth, ControlHeight, ControlFont, vehicleTypeComboBox);
            ClientSize = new Size((Spacing + ControlWidth) * 2 + Spacing, ControlHeight + Spacing * 2);
            if (mode == Mode.Add)
            {
                label1.Text = "Select vehicle type:";
                buttonText = "Add vehicle";
                Text = "Add";
            }
            else if (selectedVehicle != null) {
                label1.Text = "Vehicle type:";
                buttonText = "Save";
                SelectVehicleTypeInComboBox();
                List<Control> controls = CreateControls(selectedVehicle);
                DisplayControls(controls, this, new Point(Spacing, Spacing * 2 + ControlHeight), Spacing);
                if (mode == Mode.View)
                {
                    Text = "View";
                    DisableControls(this);
                }
                else
                {
                    Text = "Edit";
                }
            }
            CenterFormOnScreen();
        }

        private void SelectVehicleTypeInComboBox()
        {
            if (selectedVehicle != null)
            {
                string currentSelected = DisplayNameAttribute.GetDisplayName(selectedVehicle.GetType());
                for (int i = 0; i < vehicleTypeComboBox.Items.Count; i++)
                {
                    var item = vehicleTypeComboBox.Items[i];
                    if (Equals(item.ToString(), currentSelected))
                    {
                        vehicleTypeComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void DisableControls(Control owner)
        {
            foreach (Control control in owner.Controls)
            {
                if (control is GroupBox)
                {
                    DisableControls(control);
                }
                else if (control is not Label)
                {
                    control.Enabled = false;
                }
            }
        }

        private void vehicleTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedIndex = vehicleTypeComboBox.SelectedIndex;
            if (selectedIndex != -1 && selectedIndex != prevSelected)
            {
                prevSelected = selectedIndex;
                while (Controls.Count > 2)
                {
                    Controls.RemoveAt(2);
                }
                Type vehicleType = MainForm.VehiclesTypes[selectedIndex];
                var obj = Activator.CreateInstance(factories[vehicleType]);
                if (obj != null)
                {
                    VehicleFactory vehicleFactory = (VehicleFactory)obj;
                    selectedVehicle = vehicleFactory.CreateVehicle();
                    List<Control> controls = CreateControls(selectedVehicle);
                    DisplayControls(controls, this, new Point(Spacing, Spacing * 2 + ControlHeight), Spacing);
                    SetTextChangedHandler(this, controls);
                    CenterFormOnScreen();
                }
            }
        }

        private void SetTextChangedHandler(Control owner, List<Control> controls)
        {
            foreach (Control control in controls)
            {
                if (control is GroupBox)
                {
                    SetTextChangedHandler(control, control.Controls.Cast<Control>().ToList());
                }
                else
                {
                    control.TextChanged += (object? sender, EventArgs e) =>
                    {
                        if (errorProvider.GetError(sender as Control) != string.Empty)
                        {
                            errorProvider.SetError(sender as Control, string.Empty);
                        }
                    };
                }
            }
        }

        private void DisplayControls(List<Control> controls, Control owner, Point startPos, int spacing)
        {
            int x1 = startPos.X;
            int x2 = x1 + ControlWidth + spacing;
            int y = startPos.Y;
            Font italicFont = new Font(ControlFont.FontFamily, ControlFont.Size, FontStyle.Italic);
            foreach (var control in controls)
            {
                if (control is GroupBox)
                {
                    List<Control> subcontrols = control.Controls.Cast<Control>().ToList();
                    DisplayControls(subcontrols, control, new Point(startPos.X / 2, spacing * 2), spacing);
                    PlaceControl(owner, startPos.X / 2, y, control.ClientSize.Width, control.ClientSize.Height, italicFont, control);
                    y += control.Height + spacing;
                }
                else if (control is Label)
                {
                    PlaceControl(owner, x1, y, ControlWidth, ControlHeight, ControlFont, control);
                }
                else
                {
                    PlaceControl(owner, x2, y, ControlWidth, ControlHeight, ControlFont, control);
                    y += control.Height + spacing;
                }
            }
            if (owner is Form && mode != Mode.View)
            {
                DisplayButton(x1, y);
                y += ControlHeight + spacing;
            }
            owner.ClientSize = new Size((x1 + ControlWidth) * 2 + spacing, y);
        }

        private List<Control> CreateControls(object obj)
        {
            List<Control> controls = new List<Control>();
            Type type = obj.GetType();
            foreach (var property in type.GetProperties())
            {
                Type propertyType = property.PropertyType;
                ControlFactory controlFactory;
                Control[] subcontrols = Array.Empty<Control>();
                if (propertyType.IsEnum)
                {
                    controlFactory = new ComboBoxFactory();
                }
                else if (propertyType == typeof(bool))
                {
                    controlFactory = new CheckBoxFactory();
                }
                else if (propertyType == typeof(string))
                {
                    controlFactory = new TextBoxFactory();
                }
                else if (propertyType == typeof(int))
                {
                    controlFactory = new NumericUpDownFactory();
                }
                else
                {
                    controlFactory = new GroupBoxFactory();
                    if (propertyType == typeof(Engine))
                    {
                        subcontrols = CreateControls(((MotorVehicle)obj).Engine).ToArray();
                    }
                    else if (propertyType == typeof(Person))
                    {
                        subcontrols = CreateControls(((Vehicle)obj).Driver).ToArray();
                    }
                }
                var control = controlFactory.CreateControl(property);
                control.Controls.AddRange(subcontrols);
                if (control is not GroupBox)
                {
                    var labelFactory = new LabelFactory();
                    controls.Add(labelFactory.CreateControl(property));
                }
                controls.Add(control);
                controlFactory.BindToObject(control, obj, property);
            }
            return controls;
        }

        private void DisplayButton(int x, int y)
        {
            var button = new Button();
            button.Text = buttonText;
            button.Click += Button_Click;
            PlaceControl(this, x, y, ControlWidth * 2 + Spacing, ControlHeight, ControlFont, button);
        }

        private void PlaceControl(Control container, int x, int y, int width, int height, Font font, Control control)
        {
            control.Location = new Point(x, y);
            control.Font = font;
            control.Width = width;
            control.Height = height;
            container.Controls.Add(control);
        }


        private void Button_Click(object? sender, EventArgs e)
        {
            if (CheckControls(this))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }


        private bool CheckControls(Control owner)
        {
            bool result = true;
            foreach (Control control in owner.Controls)
            {
                if (control is GroupBox)
                {
                    result = CheckControls(control) && result;
                }
                else if (control is TextBox && control.Text.Length == 0 || 
                    control is NumericUpDown && ((NumericUpDown)control).Value == 0)
                {
                    errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                    errorProvider.SetIconPadding(control, 5);
                    errorProvider.SetError(control, "Fill in this field!");
                    result = false;
                }
            }
            return result;
        }
    }
}
