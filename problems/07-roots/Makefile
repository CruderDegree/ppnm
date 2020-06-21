MATHDIR = ../../math
math := $(MATHDIR)/Roots.cs
math += $(MATHDIR)/LinEqs.cs
math += $(MATHDIR)/Ode.cs
math += $(MATHDIR)/vector.cs
math += $(MATHDIR)/matrix.cs

.PHONY : all clean test

all: Answer.txt PlotB.svg

Answer.txt: test.exe main.exe
	mono $< > $@
	mono main.exe >> $@

PlotB.svg : plotb.gpi data.txt
	gnuplot $<

main.exe: main.cs lib.dll
	mcs $< -reference:lib.dll

lib.dll: $(math)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg

test: test.exe
	mono $<

test.exe: test.cs lib.dll
	mcs $< -r:lib.dll
