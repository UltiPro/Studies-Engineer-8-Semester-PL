import sympy as s
import math

s.init_printing()

x, y, e, a, b, c, z, t, AA, BB = s.symbols('x y e a b c z t AA BB') 

expr5 = x + 2*y**3/5+s.Rational(3,4)

print(expr5)