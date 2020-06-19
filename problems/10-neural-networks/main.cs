using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		// Build ANN object
		int n = 3; // Nodes
		Func<double,double> f = (x) => x*Exp(-x*x); // Activation function of neurons
		ANN ann = new ANN(n, f);

		// Generate training data
		Func<double, double> g = (z) => Sin(z); // Training function
		int N = 15; // Number of data points between a and b
		double a = 0; double b = 2*PI;
		double[] xs = new double[N+2];
		double[] ys = new double[N+2];

		xs[0] = a; ys[0] = g(a);
		//WriteLine($"x[{0}] = {xs[0]}");
		for(int i = 1; i < N + 1; i++){
			xs[i] = a + (i)*(b-a)/(N+1);
			//WriteLine($"x[{i}] = {xs[i]}");
			ys[i] = g(xs[i]);
		}
		xs[N + 1] = b; ys[N + 1] = g(b);
		//WriteLine($"x[{N + 1}] = {xs[N + 1]}");
		

		// Train ANN with data
		ann.train(xs, ys);

		// Generate plotting data
		double eps = 1.0/16;
		for(double k = a; k < b; k+=eps){
			WriteLine($"{k} {ann.evaluate(k)}");
		}
	}
}
