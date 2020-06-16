.PHONY : all clean

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs quad.dll
	mcs $< -r:quad.dll

quad.dll: Quadratures.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll
