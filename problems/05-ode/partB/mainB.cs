using System;
using static System.Console;
using static Ode;

class main{
	static void Main(String[] args){
		// Make model of the SIR for Covid-19 in Denmark
		double Tc = Double.Parse(args[0]);	// Time between contacts
		// -- Setup
		double N = 5600000; // Pop of Denmark
		double Tr = 10; // recovery time
		double h = 1;

		int tEnd = 250; // Interval 

		vector y = new vector(N-1, 1, 0); // S,I,R at t = 0 days
		vector yh = new vector(0,0,0);
		vector dy = new vector(0,0,0);

		Func<double, vector, vector> dydt = (t, yt) => 
		new vector(-(yt[0]*yt[1])/N /Tc, yt[0]*yt[1]/N/Tc - yt[1]/Tr, yt[1]/Tr);
		
		for(int t = 0; t < tEnd; t++){
			// Advance 1 day
			// Format: t, I, dI
			WriteLine($"{t} {y[1]} {dy[1]}");
			rkStep12(dydt, t, y, h, yh, dy);
			y = yh.copy();
		}
		WriteLine($"{tEnd - 1} {y[1]} {dy[1]}");
	}
}
