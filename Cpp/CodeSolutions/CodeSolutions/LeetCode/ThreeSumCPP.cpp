/*************************************************************
		Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0? Find all unique triplets in the array which gives the sum of zero.

		Note:
			The solution set must not contain duplicate triplets.

		Example:
			Given array nums = [-1, 0, 1, 2, -1, -4],

		A solution set is:
			[
			  [-1, 0, 1],
			  [-1, -1, 2]
			]
*************************************************************/
#include <iostream>
#include <vector>
using namespace std;

constexpr auto NUM_SIZE = 6;;

vector<vector<int>> ValidSolutions(int _nums[]);
void AddTriple(vector<vector<int>>* all, int first, int second, int third);
void PrintToConsole(vector<vector<int>> _threeSumTriples);
vector<vector<int>> Dedup(vector<vector<int>> threeSumTriples);

int main()
{
	int nums[NUM_SIZE] = { -1, 0, 1, 2, -1, -4 };
	PrintToConsole(Dedup(ValidSolutions(nums)));
}

/*
 * Returns a list of valid triples found within the provided array of integers.
 */
vector<vector<int>> ValidSolutions(int _nums[])
{
	vector<vector<int>> threeSumTriples;
	for (int i = 0; i < NUM_SIZE; i++)
	{
		int key = i;
		for (int j = 0; j < (NUM_SIZE - 1); j++)
		{
			if (j != key)
			{
				// Add a triple
				if ((j + 1 == key) && (j + 2 < NUM_SIZE))
				{
					AddTriple(&threeSumTriples, _nums[key], _nums[j], _nums[j + 2]);
				}
				else
				{
					AddTriple(&threeSumTriples, _nums[key], _nums[j], _nums[j + 1]);
				}
			}
		}
	}

	return threeSumTriples;
}

void AddTriple(vector<vector<int>>* all, int first, int second, int third)
{
	if (first + second + third == 0)
	{
		// Sort items to enable a de-dup check later
		if (first > second)
		{
			int temp = first;
			first = second;
			second = temp;
		}
		if (second > third)
		{
			int temp = second;
			second = third;
			third = temp;
		}
		vector<int> newList = { first, second, third };
		all->push_back(newList);
	}
}

vector<vector<int>> Dedup(vector<vector<int>> threeSumTriples)
{
	vector<vector<int>> noDups;
	vector<vector<int>>::iterator i;
	for (i = threeSumTriples.begin(); i != threeSumTriples.end(); ++i)
	{
		bool dupFound = false;
		vector<int> triple = *i;
		vector<vector<int>>::iterator j;
		for (j = noDups.begin(); j != noDups.end(); ++j)
		{
			vector<int> noDupTriple = *j;
			if (triple[0] == noDupTriple[0] &&
				triple[1] == noDupTriple[1] &&
				triple[2] == noDupTriple[2])
			{
				dupFound = true;
				break;
			}
		}

		if (!dupFound)
		{
			noDups.push_back(triple);
		}
	}

	return noDups;
}

void PrintToConsole(vector<vector<int>> _threeSumTriples)
{
	vector<vector<int>>::iterator i;
	for (i = _threeSumTriples.begin(); i != _threeSumTriples.end(); ++i)
	{
		vector<int> triple = *i;
		cout << triple[0] << "," << triple[1] << "," << triple[2] << '\n';
	}
}