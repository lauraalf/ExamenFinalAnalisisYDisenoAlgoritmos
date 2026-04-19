using System;
using System.Collections.Generic;

namespace ResolucionEcuacionesSegundoGrado
{
    /*
     * Estudiante: Laura Amelia Lora Franco
     * Matrícula: 25-0215
     * Materia: Análisis y Diseño de Algoritmos
     */
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Identificación y Formato de Salida
            Console.WriteLine("====================================================");
            Console.WriteLine($"Estudiante: Laura Amelia Lora Franco");
            Console.WriteLine($"Matrícula: 25-0215");
            Console.WriteLine($"Materia: Análisis y Diseño de Algoritmos");
            Console.WriteLine("====================================================\n");

            // 2. Definición del Vector de Entrada
            double[] datos = { 1, -2, -5, 2, -4, 10, 1, 3, -5, 2, 2, 5, 1, -1, 1 };
            List<double> raicesEncontradas = new List<double>();
            int fallosParciales = 0;

            Console.WriteLine("Iniciando procesamiento de ternas...");
            Console.WriteLine("----------------------------------------------------");

            // 3. Procesamiento en grupos de tres (a, b, c)
            for (int i = 0; i < datos.Length; i += 3)
            {
                if (i + 2 >= datos.Length) break;

                double a = datos[i];
                double b = datos[i + 1];
                double c = datos[i + 2];

                // Validación de condiciones
                // 1) Números diferentes entre sí
                bool cond1 = (a != b && a != c && b != c);
                // 2) b < 0
                bool cond2 = (b < 0);
                // 3) c es impar y múltiplo de 5
                bool cond3 = (Math.Abs(c) % 2 != 0 && Math.Abs(c) % 5 == 0);

                int contadorCondiciones = 0;
                if (cond1) contadorCondiciones++;
                if (cond2) contadorCondiciones++;
                if (cond3) contadorCondiciones++;

                // Lógica de "Fallos Parciales": exactamente 1 o 2 condiciones cumplidas
                if (contadorCondiciones == 1 || contadorCondiciones == 2)
                {
                    fallosParciales++;
                }

                Console.WriteLine($"Terna analizada: [{a}, {b}, {c}]");
                Console.WriteLine($"   Condiciones cumplidas: {contadorCondiciones}");

                // Cálculo de la Ecuación de Segundo Grado
                if (a == 0)
                {
                    Console.WriteLine("   Resultado: No es una ecuación de segundo grado (a=0).");
                }
                else
                {
                    double discriminante = (b * b) - (4 * a * c);

                    if (discriminante >= 0)
                    {
                        // Raíces reales redondeadas a 3 decimales
                        double x1 = Math.Round((-b + Math.Sqrt(discriminante)) / (2 * a), 3);
                        double x2 = Math.Round((-b - Math.Sqrt(discriminante)) / (2 * a), 3);

                        Console.WriteLine($"   Raíces calculadas: x1 = {x1:F3}, x2 = {x2:F3}");
                        raicesEncontradas.Add(x1);
                        raicesEncontradas.Add(x2);
                    }
                    else
                    {
                        Console.WriteLine("   Resultado: Posee raíces complejas.");
                    }
                }
                Console.WriteLine("----------------------------------------------------");
            }

            Console.WriteLine($"\nTotal de Fallos Parciales registrados: {fallosParciales}");

            // 4. Algoritmos de Ordenamiento y Búsqueda
            if (raicesEncontradas.Count > 0)
            {
                double[] arrayRaices = raicesEncontradas.ToArray();

                // Ordenamiento Burbuja Manual
                BubbleSortDescendente(arrayRaices);

                Console.WriteLine("\nVector de raíces ordenado de forma descendente:");
                Console.WriteLine(string.Join(" | ", arrayRaices));

                // Búsqueda Binaria para demostración
                double valorBuscado = arrayRaices[0];

                Console.WriteLine($"\nEjecutando búsqueda del valor: {valorBuscado}");

                int indiceIterativo = BusquedaBinariaIterativa(arrayRaices, valorBuscado);
                Console.WriteLine($"Resultado Búsqueda Iterativa (Índice): {indiceIterativo}");

                int indiceRecursivo = BusquedaBinariaRecursiva(arrayRaices, valorBuscado, 0, arrayRaices.Length - 1);
                Console.WriteLine($"Resultado Búsqueda Recursiva (Índice): {indiceRecursivo}");
            }
            else
            {
                Console.WriteLine("\nNo se encontraron raíces reales.");
            }

            Console.WriteLine("\nProceso finalizado.");
        }

        /// Algoritmo Burbuja (Descendente) O(n^2)
        static void BubbleSortDescendente(double[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] < arr[j + 1])
                    {
                        double temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        /// Búsqueda Binaria Iterativa O(log n)
        static int BusquedaBinariaIterativa(double[] arr, double objetivo)
        {
            int inicio = 0;
            int fin = arr.Length - 1;

            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;

                if (arr[medio] == objetivo) return medio;

                if (arr[medio] < objetivo)
                    fin = medio - 1;
                else
                    inicio = medio + 1;
            }
            return -1;
        }

        /// Búsqueda Binaria Recursiva O(log n)
        static int BusquedaBinariaRecursiva(double[] arr, double objetivo, int inicio, int fin)
        {
            if (inicio > fin) return -1;

            int medio = inicio + (fin - inicio) / 2;

            if (arr[medio] == objetivo) return medio;

            if (arr[medio] < objetivo)
                return BusquedaBinariaRecursiva(arr, objetivo, inicio, medio - 1);

            return BusquedaBinariaRecursiva(arr, objetivo, medio + 1, fin);
        }
    }
}