# Simulation-and-optimization
ATM hall queue simulator with statistical calculator, developed in Unity.

This project was developed as a college task. In it, there are three modules:

## Calculator
It uses formulas for calculating optimal parameters (waiting time, quantity of elements, odds of -n elements...) in a system, from a set of values inserted by the user.
The calculator UI was made with the UI components of Unity.

## Statistical simulation
The statistical simulation is based on Exponential distribution and Poisson distribution. They are used to create a set of values implemented in Montecarlo Simulation.

## Representation
All the values of the statistical simulation are randomly choosen using the Montecarlo algorithm, and are given to dolls that represent a real life situation in a ATM hall.
These dolls uses a NavAgent component for moving and direction.
