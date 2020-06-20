using System;
using static System.Console;
using static System.Math;
using static Ode;

class main{
	static void Main(){
		Func<double, vector, vector> f = delegate(double x, vector y){
			vector r1 = new vector(y[0], y[1]);
			vector r2 = new vector(y[2], y[3]);
			vector r3 = new vector(y[4], y[5]);
			double r12 = Pow((r2 - r1).norm(), 3);
			double r13 = Pow((r3 - r1).norm(), 3);
			double r23 = Pow((r3 - r2).norm(), 3);

			vector ddr1 = (r2 - r1) / r12 + (r3 - r1) / r13;
			vector ddr2 = (r1 - r2) / r12 + (r3 - r2) / r23;
			vector ddr3 = (r1 - r3) / r13 + (r2 - r3) / r23;
			
			double[] r = new double[] {y[6], y[7], y[8], y[9], y[10], y[11], 
				ddr1[0], ddr1[1], ddr2[0], ddr2[1], ddr3[0], ddr3[1]};
			return new vector(r);
		};
		// Initial condition
		double x1 =  -1.0024277970, x2 = 0.0041695061, v1 = 0.3489048974, v2 = 0.5306305100;
		vector y0 = new vector(new double[]{x1, x2, -x1, -x2, 0,0, v1, v2, v1, v2, -2*v1, -2*v2});
		
		// Start and end points, accuracy and step size
		double a = 0, b = 10, absAcc = 1e-3, relAcc = 1e-3, h = 1e-3;

		// Solve system
		AdaptOdeSolver Solver = new AdaptOdeSolver(f, a, y0, b, h, absAcc, relAcc);
		
		// Write data for plotting
		for(int i = 0; i < Solver.yData.Count; i++){
			Write($"{Solver.xData[i]} ");
			for(int j = 0; j < 6; j++) Write($"{Solver.yData[i][j]} ");
			Write("\n");
		}
	}
}
