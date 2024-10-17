namespace DpullaguariDS2.Views;

public partial class VHome : ContentPage
{
	public VHome()
	{
		InitializeComponent();
	}
    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        try
        {

            if (pkNombresEstudiantes.SelectedIndex == -1)
            {
                DisplayAlert("Alerta", "No hay selección", "Cerrar");
                return;
            }


            string dato = pkNombresEstudiantes.Items[pkNombresEstudiantes.SelectedIndex].ToString();
            string nota1 = ResultadoLabel.Text;
            string nota2 = ResultadoLabel2.Text;


            if (string.IsNullOrEmpty(nota1) || string.IsNullOrEmpty(nota2))
            {
                DisplayAlert("Alerta", "Faltan notas parciales para calcular la nota final", "Cerrar");
                return;
            }


            CalcularNotaFinal();


            DisplayAlert("Resultados",
                $"Estudiante: {dato}\n" +
                $"Nota Parcial 1: {nota1}\n" +
                $"Nota Parcial 2: {nota2}\n" +
                $"Nota Final: {NotaFinalLabel.Text}",
                "Cerrar");
        }
        catch (Exception ex)
        {

            DisplayAlert("Error", "Ocurrió un problema: " + ex.Message, "Cerrar");
        }
    }



    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NotaSeguimientoEntry.Text) || string.IsNullOrWhiteSpace(NotaExamenEntry.Text))
        {
            ResultadoLabel.Text = string.Empty;
            return;
        }

        if (double.TryParse(NotaSeguimientoEntry.Text, out double notaSeguimiento) &&
            double.TryParse(NotaExamenEntry.Text, out double notaExamen))
        {
            double resultado = (notaSeguimiento * 0.3) + (notaExamen * 0.2);
            ResultadoLabel.Text = resultado.ToString("F2");
        }
        else
        {
            ResultadoLabel.Text = string.Empty;
        }
    }

    private void OnEntryTextChanged2(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NotaSeguimiento2Entry.Text) || string.IsNullOrWhiteSpace(NotaExamen2Entry.Text))
        {
            ResultadoLabel2.Text = string.Empty;
            return;
        }

        if (double.TryParse(NotaSeguimiento2Entry.Text, out double notaSeguimiento2) &&
            double.TryParse(NotaExamen2Entry.Text, out double notaExamen2))
        {
            double resultado = (notaSeguimiento2 * 0.3) + (notaExamen2 * 0.2);
            ResultadoLabel2.Text = resultado.ToString("F2");
        }
        else
        {
            ResultadoLabel2.Text = string.Empty;
        }
    }

    private void CalcularNotaFinal()
    {
        if (!string.IsNullOrEmpty(ResultadoLabel.Text) &&
            !string.IsNullOrEmpty(ResultadoLabel2.Text) &&
            double.TryParse(ResultadoLabel.Text, out double parcial1) &&
            double.TryParse(ResultadoLabel2.Text, out double parcial2))
        {
            double notaFinal = (parcial1 + parcial2);
            NotaFinalLabel.Text = notaFinal.ToString("F2");


            string estado;
            if (notaFinal >= 7)
            {
                estado = "Aprobado";
            }
            else if (notaFinal >= 5 && notaFinal <= 6.9)
            {
                estado = "Complementario";
            }
            else
            {
                estado = "Reprobado";
            }


            DisplayAlert("Resultado Final", $"Nota Final: {notaFinal:F2}\nEstado: {estado}", "Cerrar");
        }
        else
        {
            NotaFinalLabel.Text = "N/A";
        }
    }
}