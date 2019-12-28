using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Botao.Clicked += BuscarCEP;
        }
        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO Validações
            string cep = CampoCEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try{
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        Resultado.Text = String.Format("Endereço: {0}, {1}, {2}", end.localidade, end.uf, end.logradouro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado" + cep, "OK");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Invalido! O CEP deve conter 8 caracteres", "OK");
                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Invalido! O CEP deve ser composto apenas por números", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
