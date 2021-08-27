using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace secondPractice.Pages
{
    public class NominaModel : PageModel
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cargo { get; set; }
        public double salario { get; set; }

        public void OnGet()
        {
        }

         public double calcularDescuentoAFP(double salario)
        {
            double montoAFP = salario * 0.0287;

            if (montoAFP > 7738.67)
            {
                montoAFP = 7738.67;
            }

            return montoAFP;
        }

        public double calcularDescuentoARS(double salario)
        {
            double montoARS = salario * 0.0304;

            if (montoARS > 4098.53)
            {
                montoARS = 4098.53;
            }

            return montoARS;
        }

        public double calcularDescuentoISR(double salario)
        {
            double montoISR = 0;
            double salarioAnual = salario * 12;
            double excedente = 0;
            double descuentoAfpAnual = calcularDescuentoAFP(salario) * 12;
            double descuentoArsAnual = calcularDescuentoARS(salario) * 12;
            salarioAnual = salarioAnual - descuentoAfpAnual - descuentoArsAnual;

            if ( (salarioAnual >= 416220.01) && (salarioAnual <= 624329.00) )
            {
                excedente = salarioAnual - 416220.01;
                montoISR = excedente * 0.15;
                montoISR = montoISR / 12;
                return montoISR;

            }
            else if( (salarioAnual >= 624329.01) && (salarioAnual <= 867123.00) )
            {
                excedente = salarioAnual - 624329.01;
                montoISR = excedente * 0.20 + 31216.00;
                montoISR = montoISR / 12;
                return montoISR;
            }
            else if (salarioAnual >= 867123.01)
            {
                excedente = salarioAnual - 867123.01;
                montoISR = excedente * 0.25 + 79776.00;
                montoISR = montoISR / 12;
                return montoISR;
            }
            else
            {
                return montoISR;
            }

        }

        public double calcularTotalDescuentos(double salario)
        {
            double montoAFP = calcularDescuentoAFP(salario);
            double montoARS = calcularDescuentoARS(salario);
            double montoISR = calcularDescuentoISR(salario);
            double totalDescuentos = montoAFP + montoARS + montoISR;
            return totalDescuentos;
        }

        public double calcularSueldoNeto(double salario)
        {
            double totalDescuentos = calcularTotalDescuentos(salario);
            double sueldoNeto = salario - totalDescuentos;
            return sueldoNeto;
        }
    }
}
