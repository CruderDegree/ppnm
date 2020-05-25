using System;
using static System.Math;

public class Fit{
	// Fields
	public vector c; // List of parameters
	public Func<double,double>[] funcs; // List of functions
	public matrix cov; // Covariance matrix of fit
	public vector cErr; // Parameter errors


	// Constructor
	public Fit(vector c, Func<double,double>[] funcs, matrix cov){
		this.c = c;
		this.funcs = funcs;
		this.cov = cov;
		this.cErr = new vector(c.size);
		for(int k = 0; k < c.size; k++){
			this.cErr[k] = Sqrt(cov[k,k]);
		}
	}

	public double evaluate(double x){
		double result = 0;
		for(int k = 0; k<c.size; k++) result += c[k]*funcs[k](x);
		return result;
	}
	public double upper(double x){
		double result = 0;
		for(int k = 0; k<c.size; k++) result += (c[k]+cErr[k])*funcs[k](x);
		return result;
	}
	public double lower(double x){
		double result = 0;
		for(int k = 0; k<c.size; k++) result += (c[k] - cErr[k])*funcs[k](x);
		return result;
	}

	public vector getParamErrors(){
		return cErr;
	}
}
