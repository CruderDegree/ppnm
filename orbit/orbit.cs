using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

class orbit{
	public static int Main(String[] args){
		// y1 has this value so ode solver takes more steps
		double a=0, b=2*PI, eps = 0, y0 = 1, y1 = -1.0/128;
		foreach(string s in args){
			string[] ws = s.Split('=');
			if(ws[0]=="eps") eps=double.Parse(ws[1]);
			if(ws[0]=="y0") y0=double.Parse(ws[1]);
			if(ws[0]=="y1") y1=double.Parse(ws[1]);
		}

		Func<double, vector, vector> orb = (x,y) => 
				new vector(y[1], 1-y[0] + y[0]*y[0]*eps);
		vector ya = new vector(y0, y1);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		ode.rk23(orb, a,ya,b, xlist:xs, ylist:ys, h:0.01);
		for(int i = 0; i<xs.Count; i++){
			WriteLine($"{xs[i]} {ys[i][0]}");
		}
		return 0;
	}
}

