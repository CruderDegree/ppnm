using System;
using static System.Console;
using static System.Math;

public class ANN{
	// One hidden layer, feed forward ANN
	// --- Fields
	public int n; // Number of hidden neurons
	public vector p; // Vector containing parameters (a, b, w, a, b, w, ...)
	public Func<double, double> f; // Activation function of neurons
	public Func<double, double> dfdx; // Differentiated activation function
	public Func<double, double> intf; // Integrated activation function
	
	public ANN(int n, Func<double,double> f, Func<double, double> dfdx, Func<double,double> intf){
		this.n = n;
		this.f = f;
		this.dfdx = dfdx;
		this.intf = intf;
		this.p = new vector(3*n);
		for(int i = 0; i < p.size; i++) p[i] = 4; // Set all params to 4
	}

	public double evaluate(double x){
		double res = 0;
		double a, b, w;
		for(int i = 0; i < n; i++){
			a = p[3*i];
			b = p[3*i + 1];
			w = p[3*i + 2];
			res += w * f((x-a)/b);
		}
		return res;
	}

	public double evaluateDiff(double x){
		double res = 0;
		double a, b, w;
		for(int i = 0; i < n; i++){
			a = p[3*i];
			b = p[3*i + 1];
			w = p[3*i + 2];
			res += w/b * dfdx((x-a)/b);
		}
		return res;
	}

	public double evaluateInt(double x){
		double res = 0;
		double a, b, w;
		for(int i = 0; i < n; i++){
			a = p[3*i];
			b = p[3*i + 1];
			w = p[3*i + 2];
			res += w * b * intf((x-a)/b);
		}
		return res;
	}

	public void train(double[] x, double[] y){
		Func<vector, double> dp = delegate(vector v){
			// Deviation from tabulated values given parameters v
			double sum = 0;
			p = v; // Set ANN parameters to those given
			for(int i = 0; i < x.Length; i++){
				sum += Pow(evaluate(x[i]) - y[i],2);
			}
			return sum;
		};

		(vector bestP, int steps) = Minimization.qNewton(dp, p, 1.0/100);
		p = bestP;
	}
}
