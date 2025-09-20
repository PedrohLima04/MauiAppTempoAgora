using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sem conexão", "Verifique sua internet e tente novamente.", "OK");
                return; 
            }

            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Visibilidade: {t.visibility} \n" +
                                         $"Velocidade do Vento: {t.speed} \n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {

                        lbl_res.Text = "Cidade não encontrada";
                        await DisplayAlert("Cidade não encontrada", "Digite o nome correto da Cidade", "OK");
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
