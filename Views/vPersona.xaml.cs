using aharoT5.Models;

namespace aharoT5.Views;

public partial class vPersona : ContentPage
{
    private PersonRepository personRepository;

    public vPersona()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");
        personRepository = new PersonRepository(dbPath);

    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = " ";
        App.personRepo.AddNewPerson(txtName.Text);
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = " ";
        List<Persona> people = App.personRepo.getAllPeople();
        listaPersona.ItemsSource = people;
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private async  void btnActualizar_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var id = button?.CommandParameter as Persona;
        var persona = button?.CommandParameter as Persona;

        if (persona != null)
        {
            string nuevoNombre = await DisplayPromptAsync("Actualizar Persona", "Ingrese el nuevo nombre:", "Aceptar", "Cancelar", "Nuevo Nombre");

            if (nuevoNombre != null) 
            {
                persona.Name = nuevoNombre; 


                personRepository.UpdatePerson(id,persona);

                listaPersona.ItemsSource = personRepository.getAllPeople();
            }
            
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var id = button?.CommandParameter as Persona;
        var persona = button?.CommandParameter as Persona;

        if (persona != null)
        {
            personRepository.DeletePerson(id,persona);

            listaPersona.ItemsSource = personRepository.getAllPeople();
        }
    }
}