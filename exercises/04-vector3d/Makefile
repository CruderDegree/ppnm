.PHONEY:all
all: out.txt
	cat out.txt

out.txt : main.exe
	mono main.exe > out.txt

main.exe : main.cs vector3d.dll ivector3d.dll
	mcs -reference:vector3d.dll -r:ivector3d.dll main.cs 

vector3d.dll: vector3d.cs ivector3d.dll
	mcs -reference:ivector3d.dll $< -target:library

ivector3d.dll: ivector3d.cs
	mcs $< -target:library

.PHONEY:clean
clean: 
	rm --force out.txt main.txt
