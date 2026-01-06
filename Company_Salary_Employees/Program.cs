// See https://aka.ms/new-console-template for more information
using System;

namespace Company_Salary_Employees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Δημιουργία λίστας με όλους τους υπαλλήλους, προσδιορίζοντας όνομα, τμήμα και μισθό ετήσιο.
            var employees = new List<Employees>
            {
                new Employees ("Maria Iakovidi", "FrontEnd Development", 14000.30m),
                new Employees ("Alexios Athanasiou", "Risk Assesment", 13000.54m),
                new Employees ("Stefanos Alexiadis", "FrontEnd Development", 15000.74m),
                new Employees ("Stavroula Christou", "Backend Development", 20000.35m),
                new Employees ("Iasonas Delligiannis", "App Development", 18000.65m),
                new Employees ("Ifigeneia Papadopoulou", "Full-Stack Development", 15000.78m),
                new Employees ("Iraklis Efstathiou", "Data Analysis", 21000.50m)
            };

            Dictionary<string, decimal> employeesalaryprint = EmployeesAverageSalaryCalculation.CalculateAverageSalaryForDepartment(employees);
            foreach(var salary in employeesalaryprint)
            {
                Console.WriteLine($"Department: {salary.Key}, Average Salary Per Department: {salary.Value}");
            }

            Console.ReadKey();
        }
    }

    // Δημιουργία κλάσης Employees.
    public class Employees
    {
        // Δημιουργία των ιδιοτήτων.
        public string Name {  get; }
        public string Department { get; }
        public decimal AnnualSalary {  get; }

        // Δημιουργία του κατασκευαστή.
        public Employees(string name, string department, decimal monthlySalary)
        {
            Name = name;
            Department = department;
            AnnualSalary = monthlySalary;
        }
    }

    // Δημιουργία κλάσης EmployeesAverageSalaryCalculation, η οποία θα περιέχει την μέθοδο για τον υπολογισμό του μέσου όρου μισθού του κάθε τμήματος.
    public static class EmployeesAverageSalaryCalculation
    {
        // Θα πρέπει να φτιαχτεί μία μέθοδος επαναχρησιμοποιούμενη, η οποία με βάση την λίστα με τους Employees, θα τους γκρουπάρει τους υπαλλήλους και θα υπολογίζει για κάθε τμήμα τον μέσο όρο μισθού.
        // Η μέθοδος αυτή δέχεται ως παράμετρο εισόδου μία λίστα, δηλαδή την λίστα με τους Employees και επιστρέφει τον μέσο όρο ετήσιου μισθού τους με τύπο επιστρεφόμενης τιμής ένα dictionary (λεξικό), το οποίο θα περιέχει ως key (το τμήμα) και ως value (τον μέσο όρο ετήσιου μισθού που αντιστοιχεί στο κάθε τμήμα).
        public static Dictionary<string, decimal> CalculateAverageSalaryForDepartment(List<Employees> employees)
        {
            Dictionary<string, List<Employees>> groupByDep = new Dictionary<string, List<Employees>>();    // Δημιουργία ενός λεξικού (Dictionary), που λαμβάνει ως ορίσματα ένα string (το οποίο αντιπροσωπεύει το department) και μία λίστα με τους υπαλλήλους και το αρχικοποιώ ως ένα κενό λεξικό (dictionary), όπου το όρισμα string αντιπροσωπεύει το τμήμα, ενώ το value είναι η λίστα με τα αντικείμενα που ανήκουν σε αυτό το τμήμα.
            foreach (var employee in employees)    // Για κάθε εργαζόμενο σε αυτή την λίστα θέλουμε να κατασκευάσουμε ένα dictionary
            {
                if (!groupByDep.ContainsKey(employee.Department))    // Εδώ ελέγχω μέσω της μεθόδου ContainsKey εάν ένας υπάλληλος δεν υπάρχει σε ένα τμήμα και στην περίπτωση που δεν υπάρχει τότε μέσα στο dictionary βάζω ένα νέο key που δεν υπάρχει.
                {
                    groupByDep[employee.Department] = new List<Employees>();   // Εδώ φτιάχνω μία κενή λίστα μόνο για τον πρώτο υπάλληλο που μπαίνει μέσα στην λίστα.
                }
                groupByDep[employee.Department].Add(employee);    // Σε αυτό το dictionary (groupByDep), θέλω στο key (που είμαστε σίγουροι ότι υπάρχει) είτε είναι ο πρώτος υπάλληλος είτε είναι ένας υπάλληλος που είμαστε σίγουροι ότι έχει φτιαχτεί ένα key για αυτόν τον υπάλληλο και μέσω της μεθόδου Add (προσθέτω τον employee στην λίστα αυτήν εδώ: employee). 
            }

            var result = new Dictionary<string, decimal>();     // Δημιουργία της τελικής λίστας, δηλαδή του τελικού Dictionary.
            foreach (var employeesperdepartment in groupByDep)   // Προσπέλαση σε κάθε αντικείμενο του προηγούμενου dictionary.
            {
                decimal sumOfSalaries = 0;     // Δημιουργία μετρητή και αρχικοποίησή του από το 0. Αυτός ο μετρητής θα μού κρατάει το άθροισμα των μισθών του κάθε υπαλλήλου.

                foreach (var employee in employeesperdepartment.Value)
                {
                    sumOfSalaries += employee.AnnualSalary;    // Πρόσθεση όλων των μισθών.
                }
                var salaryAverage = sumOfSalaries / employeesperdepartment.Value.Count;     // Εδώ διαιρείται ο αριθμός των υπαλλήλων.
                result[employeesperdepartment.Key] = salaryAverage;     // Αποθήκευση του αποτελέσματος μέσα στην μεταβλητή result.
            }

            return result;     // Σε αυτό το σημείο επιστρέφεται το τελικό Dictionary με τους μέσους όρους του κάθε τμήματος.
        }
    }   // Εδώ κλείνει η μέθοδος.
}
