all: Plot.svg Orbits.svg

Orbits.svg: orbits.gpi out.circle.txt out.ellipse.txt out.genrel.txt
	gnuplot $<

out.circle.txt: orbit.exe
	mono $< > $@ 2>> orbit.error.txt

out.ellipse.txt: orbit.exe
	mono $< eps=0 y0=1 y1=-0.5 > $@ 2>> orbit.error.txt

out.genrel.txt: orbit.exe
	mono $< eps=0.01 y0=1 y1=-0.5 > $@ 2>> orbit.error.txt

orbit.exe: orbit.cs mathlib.dll
	mcs $< -r:mathlib.dll

Plot.svg: ode-plot.gpi ode.out.txt ode.table.txt
	gnuplot $<

ode.out.txt: ode-main.exe
	mono ode-main.exe > $@ 2> ode.error.txt

ode.table.txt: ode-table.exe
	mono $< > $@ 2>> ode.error.txt

ode-main.exe: ode-main.cs mathlib.dll
	mcs $< -r:mathlib.dll

ode-table.exe: ode-table.cs
	mcs $<

mathlib.dll: ode.cs vector.cs
	mcs $^ -target:library -o $@

clean:
	rm -f *.exe *.txt *.dll *.svg

