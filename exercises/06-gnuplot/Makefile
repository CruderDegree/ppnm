all: Error.svg Gamma.svg LnGamma.svg ComplexGamma.svg

ComplexGamma.svg: complexgamma.gpi out.complexgamma.txt
	gnuplot $<

Error.svg: error.gpi out.error.txt error.tab.txt
	gnuplot $<

Gamma.svg: gamma.gpi out.gamma.txt gamma.tab.txt
	gnuplot $<

LnGamma.svg: lngamma.gpi out.lngamma.txt lngamma.tab.txt
	gnuplot $< 

out.complexgamma.txt: main-complexgamma.exe
	mono $< > $@

out.error.txt : main-error.exe
	mono $< > $@

out.gamma.txt : main-gamma.exe
	mono $< > $@

out.lngamma.txt : main-lngamma.exe
	mono $< > $@

main-complexgamma.exe: main-complexgamma.cs math.dll
	mcs $< -reference:math.dll -reference:cmath.dll

main-error.exe: main-error.cs math.dll
	mcs $< -reference:math.dll

main-gamma.exe: main-gamma.cs math.dll
	mcs $< -reference:math.dll

main-lngamma.exe: main-lngamma.cs math.dll
	mcs $< -reference:math.dll

math.dll: error.cs cmath.dll
	mcs $< -target:library -out:$@ -reference:cmath.dll

.PHONEY:clean
clean:
	rm --force *.exe *.svg math.dll out*
