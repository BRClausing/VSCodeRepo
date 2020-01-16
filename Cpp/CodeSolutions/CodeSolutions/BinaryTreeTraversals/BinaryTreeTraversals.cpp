#include <stdio.h>
#include <iostream>
#include <stack>
#include <list>
using namespace std;

// BinaryTreeTraversals.cpp : Defines the entry point for the console application.

struct BinaryTree
{
	BinaryTree* pLeft;
	BinaryTree* pRight;
	int iValue;
};

BinaryTree* buildNode( int iData, BinaryTree* pLeftChild, BinaryTree* pRightChild)
{
	BinaryTree* pNode = new BinaryTree;

	pNode->iValue = iData;
	pNode->pLeft = pLeftChild;
	pNode->pRight = pRightChild;

	return pNode;
}

BinaryTree* buildBinaryTree( void )
{
	// This will build a very specific tree for testing
	BinaryTree *pRootNode, *pLeftNode, *pRightNode, *pSaveNode;
	// build bottom level nodes first
	pLeftNode = buildNode( 14, NULL, NULL);
	pRootNode = buildNode( 11, pLeftNode, NULL);
    
	pLeftNode = pRootNode;
	pRightNode = buildNode( 15, NULL, NULL);
	pRootNode = buildNode( 4, pLeftNode, pRightNode);
	pSaveNode = pRootNode;

    pRightNode = buildNode( 19, NULL, NULL);
	pLeftNode = buildNode( 17, NULL, pRightNode);
	pRootNode = buildNode( 10, pLeftNode, NULL);
	pRightNode = pRootNode;

	pRootNode = buildNode( 1, pSaveNode, pRightNode);
	
	pLeftNode = pRootNode;
	pRightNode = buildNode( 6, NULL, NULL);
	pRootNode = buildNode( 9, pLeftNode, pRightNode);
	pSaveNode = pRootNode;

	pLeftNode = buildNode( 20, NULL, NULL);
	pRootNode = buildNode( 2, pLeftNode, NULL);
	pLeftNode = pRootNode;
	pRootNode = buildNode( 3, pLeftNode, NULL);

	pRightNode = pRootNode;
	pLeftNode = buildNode( 8, NULL, NULL);
	pRootNode = buildNode( 7, pLeftNode, pRightNode);

	pRightNode = pRootNode;
	pRootNode = buildNode( 5, pSaveNode, pRightNode);

	return pRootNode;
}

// In order traversal = LVR
void InOrderTrav( BinaryTree* pbt )
{
	if (pbt->pLeft != NULL)
		InOrderTrav( pbt->pLeft );

	printf("%d, ", pbt->iValue);

	if (pbt->pRight != NULL)
		InOrderTrav( pbt->pRight );
}

// Post-Order traversal = LRV
void PostOrderTrav( BinaryTree* pbt )
{
	if (pbt->pLeft != NULL)
		PostOrderTrav( pbt->pLeft );

	if (pbt->pRight != NULL)
		PostOrderTrav( pbt->pRight );

	printf("%d, ", pbt->iValue);
}

// Pre-Order traversal = VLR
void PreOrderTrav( BinaryTree* pbt )
{
	printf("%d, ", pbt->iValue);

	if (pbt->pLeft != NULL)
		PreOrderTrav( pbt->pLeft );

	if (pbt->pRight != NULL)
		PreOrderTrav( pbt->pRight );
}

// helper function for BottomUpListTrav
void printBottomUpList( list<BinaryTree*> oldList)
{
	list<BinaryTree*> newList;
	list<BinaryTree*>::iterator listIter;
	BinaryTree* pTmp;

	for( listIter = oldList.begin(); listIter != oldList.end(); listIter++ )
	{
		pTmp = *listIter;
		// store both children
		if (pTmp->pLeft)
			newList.push_back(pTmp->pLeft);

		if (pTmp->pRight)
			newList.push_back(pTmp->pRight);
	}

	if ( !newList.empty() )
		printBottomUpList( newList );

	for( listIter = oldList.begin(); listIter != oldList.end(); listIter++ )
	{
		// print out the values of the node
		cout << (*listIter)->iValue << ", ";
	}

}
// print out node values on each level starting from the bottom
// This version will use a list and a helper function
void BottomUpListTrav( BinaryTree* pbt )
{
	list<BinaryTree*> pbtList;

	pbtList.push_back(pbt);

	printBottomUpList(pbtList);
}

// Helper function for TopDownListTrav()
void printTopDownList( list<BinaryTree*> oldList)
{
	list<BinaryTree*> newList;
	list<BinaryTree*>::iterator listIter;
	BinaryTree* pTmp;

	for( listIter = oldList.begin(); listIter != oldList.end(); listIter++ )
	{
		// print out the values of the node
		cout << (*listIter)->iValue << ", ";
	}

	for( listIter = oldList.begin(); listIter != oldList.end(); listIter++ )
	{
		pTmp = *listIter;
		// store both children
		if (pTmp->pLeft)
			newList.push_back(pTmp->pLeft);

		if (pTmp->pRight)
			newList.push_back(pTmp->pRight);
	}

	if ( !newList.empty() )
		printTopDownList( newList );

}
// Same as BottomUpListTrav, but starting at the root node and working down
void TopDownListTrav( BinaryTree* pbt )
{
	list<BinaryTree*> pbtList;

	pbtList.push_back(pbt);

	printTopDownList(pbtList);
}
// print out node values on each level starting from the top
// This version will convert the tree to an array, then access
// the array values by level using 2^level-1 formual
void TopDownArrayTrav( BinaryTree* pbt )
{

}

// print out node values on each level starting from the top
// This version will pass the curr env, the user will print it
// at the end
stack<int> TopDownEnvTrav( BinaryTree* pbt )
{
	stack<int> intStack;

	return intStack;
}

int main(int argc, char* argv[])
{
	BinaryTree* pBT = buildBinaryTree();

	cout << "Binary Tree Traversals!" << endl;
	cout << "Direction  = Results" << endl << endl;

	cout << "In-Order   = ";
	InOrderTrav( pBT );
	cout << endl << endl;

	cout << "Post-Order = ";
	PostOrderTrav( pBT );
	cout << endl << endl;

	cout << "Pre-Order  = ";
	PreOrderTrav( pBT );
	cout << endl << endl;

	cout << "Bottom-Up  = ";
	BottomUpListTrav( pBT );
	cout << endl << endl;

	cout << "Top-Down   = ";
	TopDownListTrav( pBT );
	cout << endl << endl;

	int tmp = 0;
	cin >> tmp;

	return 0;
}
