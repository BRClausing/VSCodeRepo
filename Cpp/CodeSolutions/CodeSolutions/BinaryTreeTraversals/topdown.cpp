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

void TopDownListTrav( BinaryTree* pbt )
{
	list<BinaryTree*> pbtList;

	pbtList.push_back(pbt);

	printTopDownList(pbtList);
}

int main(int argc, char* argv[])
{
	// buildBinaryTree() creates a tree to traverse
	BinaryTree* pBT = buildBinaryTree();

	TopDownListTrav( pBT );

	return 0;
}
