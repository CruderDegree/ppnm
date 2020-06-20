MATHDIR = ../../../math/

.PHONY : all clean

all: PlotC.svg

PlotC.svg : plotc.gpi data.txt
	gnuplot $<

data.txt : main.exe
	mono $< > $@

main.exe : main.cs lib.dll
	mcs $< -r:lib.dll -out:$@

lib.dll : $(MATHDIR)Ode.cs $(MATHDIR)vector.cs
	mcs $^ -target:library -out:$@

clean: 
	rm -f *.svg *.exe *.txt *.dll
