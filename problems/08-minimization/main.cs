using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using static Minimization;
using System;

class main{
	static void Main(){
		Func<vector, double> rosenBrock = delegate(vector z){
			double x = z[0], y = z[1];
			return Pow(1-x, 2) + 100 * Pow(y - x*x, 2);
		};
		vector p = new vector(0.0, 0.0);
		double eps = 1.0/1000;
		(vector roots,int steps)= qNewton(rosenBrock, p, eps);
		WriteLine("--- Part A ---");
		WriteLine("Searching for a minimum of the Rosenbrock Valley function...");
		p.print("Starting point:");
		roots.print($"Minimum found ({steps} steps)");
		WriteLine("Global minimum: (1,1)");

		Func<vector, double> himmelblau = delegate(vector z){
			double x = z[0], y = z[1];
			return Pow(x*x + y - 11, 2) + Pow(x + y*y - 7, 2);
		};
		p = new vector(0.0, 0.0);
		eps = 1.0/1000;
		(roots,steps)= qNewton(himmelblau, p, eps);
		WriteLine("Searching for a minimum of the Himmelblau function...");
		p.print("Starting point:");
		roots.print($"Minimum found ({steps} steps)");
		WriteLine("Local minimum: (3,2)");

		WriteLine("\n\n--- Part B ---");
		// Read higgs.txt for fitting data
		StreamReader Reader = new StreamReader("higgs.txt");
		int n = 30;
		vector E = new vector(n), sigma = new vector(n), err = new vector(n);
		for(int i = 0; i < n; i++){
			string line = Reader.ReadLine();
			string [] data = line.Split(new char [] {' '});
			E[i] = (Double.Parse(data[0]));
			if(data[1] == ""){
				sigma[i] = Double.Parse(data[2]);
				err[i] = Double.Parse(data[3]);
			}
			else{ 
				sigma[i] = (Double.Parse(data[1]));
				err[i] = (Double.Parse(data[2]));
			}
		}
		Reader.Close();
 
		Func<double, double, double, double, double> breitWigner = (e, m, g, a) =>
			a/(Pow(e-m,2) + g*g/4);

		Func<vector, double> deviation = delegate(vector z){
			double m = z[0];
			double gamma = z[1];
			double A = z[2];
			double sum = 0;
			for(int i = 0; i < n; i++){
				sum += Pow(breitWigner(E[i], m, gamma, A) - sigma[i], 2);
			}
			return sum;
		};

		p = new vector(125, 2, 6);
		vector min;
		(min, steps) = qNewton(deviation, p, eps);
		WriteLine($"Fitting Higgs data to Breit-Wigner function, done in {steps} steps.");
		WriteLine($"Found parameters:");
		WriteLine($"Mass: {min[0]} \nGamma: {min[1]} \nA: {min[2]}");

		WriteLine("\n\n--- Part C ---");
		WriteLine("Find minimum Rosenbrock Valley function...");
		p = new vector(0.0, 0.0);
		eps = 1e-3;
		DownhillSimplex DHS = new DownhillSimplex(rosenBrock, p, eps);
		DHS.x.print($"Minimum found ({DHS.steps} steps):");
	}
}
