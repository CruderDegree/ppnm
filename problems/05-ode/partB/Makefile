#Makefile of Ode part B
MATHDIR = ../../../math/

.PHONY : all clean

all: PlotB.svg

PlotB.svg: plotb.gpi data1.txt data2.txt data3.txt data4.txt data5.txt
	gnuplot $<

data1.txt: mainB.exe
	mono $< 1 > $@

data2.txt: mainB.exe
	mono $< 2 > $@

data3.txt: mainB.exe
	mono $< 3 > $@

data4.txt: mainB.exe
	mono $< 4 > $@

data5.txt: mainB.exe
	mono $< 5 > $@

mainB.exe: mainB.cs lib.dll
	mcs $< -reference:lib.dll -o:$@

lib.dll: $(MATHDIR)Ode.cs $(MATHDIR)vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.svg *.txt *.exe *.dll
