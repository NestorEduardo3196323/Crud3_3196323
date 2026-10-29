namespace Crud3_3196323
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editTrabajadorId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetTrabajadores());
        }

        private async void guardarBtn_Clicked(object sender, EventArgs e)
        {
            //Calcula el sueldo semanal: horas trabajadas por el pago por hora
            double.TryParse(horasEntry.Text, out double horas);
            double.TryParse(pagoEntry.Text, out double pago);
            double sueldo = horas * pago;
            sueldoLabel.Text = sueldo.ToString();

            if (_editTrabajadorId == 0)
            {
                //agrega trabajador
                await _dbService.Create(new Trabajador
                {
                    Nombre = nombreEntry.Text,
                    Horas = horas,
                    PagoHora = pago,
                    Sueldo = sueldo
                });
            }
            else
            {
                //Edita trabajador
                await _dbService.Update(new Trabajador
                {
                    Id = _editTrabajadorId,
                    Nombre = nombreEntry.Text,
                    Horas = horas,
                    PagoHora = pago,
                    Sueldo = sueldo
                });
                _editTrabajadorId = 0;
            }
            nombreEntry.Text = string.Empty;
            horasEntry.Text = string.Empty;
            pagoEntry.Text = string.Empty;
            //El sueldo se queda visible en el label para que lo puedas ver

            listview.ItemsSource = await _dbService.GetTrabajadores();
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var trabajador = (Trabajador)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editTrabajadorId = trabajador.Id;
                    nombreEntry.Text = trabajador.Nombre;
                    horasEntry.Text = trabajador.Horas.ToString();
                    pagoEntry.Text = trabajador.PagoHora.ToString();
                    sueldoLabel.Text = trabajador.Sueldo.ToString();
                    break;

                case "Delete":
                    await _dbService.Delete(trabajador);
                    listview.ItemsSource = await _dbService.GetTrabajadores();
                    break;
            }
        }
    }
}
