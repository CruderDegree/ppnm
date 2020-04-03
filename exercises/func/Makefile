all: out.main.txt Quantum.svg

Quantum.svg: quantum.gpi
	gnuplot $<

out.quantum.txt qmerrorlog.txt: main-quantum.exe
	mono $< > out.quantum.txt 2> qmerrorlog.txt

main-quantum.exe: main-quantum.cs quad.dll
	mcs $< -r:quad.dll

out.main.txt errorlog.txt: main.exe
	mono main.exe > out.main.txt 2>errorlog.txt

main.exe: main.cs quad.dll
	mcs $< -reference:quad.dll

quad.dll: quado8.cs
	mcs $< -target:library -out:$@

clean:
	rm --force *.exe *.txt *.dll
