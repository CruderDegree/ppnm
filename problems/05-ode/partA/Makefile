MATHDIR = ../../../math/

.PHONY : all clean

all: PlotA.svg

PlotA.svg: plotA.gpi data.txt
	gnuplot $<

data.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll

# Make math lib file
lib.dll: $(MATHDIR)Ode.cs $(MATHDIR)vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.svg *.txt
