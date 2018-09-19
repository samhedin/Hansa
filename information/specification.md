# Hansa specification

## Leagues

A league is made up of cities. Cities within a league can trade with each other.
Transportation costs (in labor) depend on the particular city pair - typically transportation cost is larger the farther apart the cities are and is much smaller by water than by land. Transportation is instantaneous, each turn represents a year so it makes sense.
The labor cost for transportation is provided by the exporting city.

Trade occurs only within a league.

## Cities

A city has a population, a production function, and a utility function.
The production function uses labor to produce any of several goods. A particular amount of labor is needed to produce one unit of a good. N times the laber produces N units of the good.
The labor cost per unit is different for different goods within one city, and for the same good in different cities.
Cost for a particular city is partly, but not entirely, determined by the surrounding terrain.

### Utility

A city's utility function represents the total happiness of the inhabitants.
The additional utility from consuming one more unit of a good decreases as the amount of the consumed good increases and increases as the amount of any other good increases.
The utility function is identical across cities. A city's "autarchy level" is the highest level of utility it can achieve without trade.

## Player interaction

Trade, production and consumption are organized by the player. The player instructs each city in his league on how to allocate its annual labor among different goods and which of the goods it produces should be shipped to other cities. Goods cannot be stored, so consumption is production plus goods shipped from other cities minus goods shipped to other cities.

## League expansion and contraction

A league may at any time send trade missions to try to persuade non-member cities to join. Supporting a trade mission costs a certain amount of labor. A mission starts from some member city of the league; that city must provide the labor. Missions to more distant cities cost more.

Cities can joind and leave leagues. One reason for leaving is that a city concludes it would be better off on its own. This may happen in any year that the city's utility is lower than its autarchy level - the utility it would have if it engaged in no trade at all. 

A second reason for leaving a league is to join another. If a member city of a league receives a mission from a different league, the probability that it will accept is an increasing function of the difference between the weighted aerage utility gain of the cities in the inviting league and the utility of that city.
Thus a player who wishes to defend cities on the frontier between his league and a neighboring league may do so by keeping them happy - arranging trade so that their utility gain from membership in his league is large.

A city that sends a trade mission may increase the probability of success by guaranteeing the city it is inviting a specified level of utility, defined as a number of points above autarchy. If the offer is accepted an the guarantee is not fulfilled - if, in other words, the new city's utility is allowed to fall below the guaranteed level - the city is likely to secede from the league, just as a city is likely to seced if its utility is allowed to drop below autarchy.

## Scoring, alternatives.

1. Hansa is a solitaire game; the objective is to get as large a score as possible. The game starts with a league
of two adjacent cities controlled by the player; all other cities are autarchic, and remain so unless annexed by
the league. The score is the utility gain resulting from the league, summed over cities and time. Alternatively,
the objective might be to bring all of the world into the league as rapidly as possible.

2. Hansa is an N-player game. Other players are either the computer, playing its own league, or humans, or
both. Victory is defined either as eliminating all other players and getting all cities into your league, or as
having the highest score (as in 1) at the end of a given number of move