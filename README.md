# SpreadSheet

## Credit: 
This project is designed by the curriculum of CS3500 from School of Computing, University of Utah. 
This project is implemented by Albert Liu. 

## Project Description
A clone of microsoft Spreadsheet tool with basic features: 
1. the user can input values into the cells. The user can click the cell, and input either some text, a number, or a formula into it. 
2. the user can perform basic arithmatic operations. "=" will indicate the start of a formula in that cell. 
3. the user can perform dependency on cells to make one cell depending on another. By using the "=" to reference one cell to another, 
changes to the value of a certain cell will change the value of any cells depending on it. 

## references: 
feature number 2 uses the FormulaParser2.0 package built by myself. See the repository in the link below. 
https://github.com/albertNightingale/FormulaParser2.0

feature number 3 uses the DependencyGraph package built by myself. See the repository in the link below. 
https://github.com/albertNightingale/DependencyGraph



