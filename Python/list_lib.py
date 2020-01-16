# return the first element of a list
def car(list):
	return list[0]

# return the "rest" of the list, i.e. (the whole list) - (the first element)	
def cdr(list):
	return list[1:]

# prepend an element onto a list
def cons(x, list):
	TempList = [x]
	return (merge(TempList, list))

# add1 will return an int with a value of 1 more than n
def add1(n):
	return (n + 1)

###########################################################################
# list predicates
#
# is the list empty?	
def isNull(list):
	return (len(list) == 0)

def isNumber(obj):
	return (type(obj) == type(1))
	
def isSymbol(obj):
	return (type(obj) == type('str'))

def isList(obj):
	return (type(obj) == type([1,2]))
	
def isNum_GT(a, b):
	return (a > b)

def isNum_LT(a, b):
	return (a < b)

###########################################################################
# list functions
#
# reverse_append that takes two strings, need a version that takes two lists
# str1 has all its characters reversed and prepended onto str2
def reverse_append(str1, str2):
	if (len(str1)) == 0: return str2
	str2 = str1[0] + str2
	return (reverse_append(str1[1:], str2))
	
# merge list two onto the end of list one
def merge(lst1, lst2):
	if (len(lst2)) == 0: return lst1
	lst1.append(lst2[0])
	return (merge(lst1, lst2[1:]))
	
# insert n into the list of numbers lon based on the predicate pred
def insert_lon(pred, n, lon):
	if (isNull(lon)): return [n]
	if pred(car(lon), n):
		return (cons(car(lon), insert_lon( pred, n, cdr(lon))))
	else:
		return (cons(n, lon))

# duple returns a list containing n copies of item
def duple(n, item):
	if n == 0: return []
	return (cons(item, duple(n-1, item)))

# takes a list of two items and returns a list of the two items swapped.
def swap_pair( two_list ):
	return (cons(car(two_list[1:2]), cons(car(two_list[0:1]), [])))

# returns a list with each 2-list reversed
# lst is a list of 2-lists
def invert(lst):
	if isNull(lst): return []
	return (cons(swap_pair(car(lst)), invert(cdr(lst))))

# returns the list of those elements in lst that satisfy the predicate pred.
# The only predicates supported as of now are isNumber and isSymbol
def filter_in(pred, lst):
	if isNull(lst): return []
	if pred(car(lst)): return (cons(car(lst), filter_in(pred, cdr(lst))))
	else: return (filter_in(pred, cdr(lst)))
	
# returns 0 (false) if any element of lst fails to satisfy pred, and returns
# 1 (true) otherwise
# pred can be any predicate that returns T/F.  Like those defined in this module.
def every( pred, lst):
	if isNull(lst): return 1
	if pred(car(lst)): return (every(pred, cdr(lst)))
	else: return 0

# returns 1 (true) if any element of lst satisfies pred, and returns
# 0 (false) otherwise
# pred can be any predicate that returns T/F.  Like those defined in this module.
def exists(pred, lst):
	if isNull(lst): return 0
	if pred(car(lst)): return 1
	else: return(exists(pred, cdr(lst)))

# wraps brackets around each top-level element of lst
def down(lst):
	if isNull(lst): return []
	return (cons(cons(car(lst), []), down(cdr(lst))))

# removes a pair of brackets from each top-level element of lst.  If a top-
# level element is not a list, it is included in the result, as is.  The value
# of (up(down lst)) is equivalent to lst, but (down(up lst)) is not necessarily
# lst.
# examples:
# up([[1,2],[3,4]])
# [1, 2, 3, 4]
# up(['x', ['y'], 'z'])
# ['x', 'y', 'z']
# up([['x', ['y']], 'z'])
# ['x', ['y'], 'z']
def up(lst):
	if isNull(lst): return []
	if isList(car(lst)): return (merge(car(lst), up(cdr(lst))))
	else: return (cons(car(lst), up(cdr(lst))))

# returns a list the same as slist, but with all occurrences of s1 replaced
# by s2 and all occurrences of s2 replaced by s1.
# example:
# swapper('x', 'y', [['x'],'y',['z', ['x']]])
# [['y'], 'x', ['z', ['y']]]
def swapper(s1, s2, slist):
	if isNull(slist): return []
	if isSymbol(car(slist)):
		if (car(slist) == s1): return (cons(s2,swapper(s1, s2, cdr(slist))))
		elif (car(slist) == s2):
			return (cons(s1, swapper(s1, s2, cdr(slist))))
		else: return (cons(car(slist), swapper(s1, s2, cdr(slist))))
	else:
		return(cons(swapper(s1, s2, car(slist)), swapper(s1, s2, cdr(slist))))

# returns the number of occurrences of s in slist.
# example:
# count_occurrences('x', [['f','x'], 'y', [[['x', 'z'], 'x']]])
# 3
def count_occurrences(s, slist):
	if isNull(slist): return 0
	if isList(car(slist)):
		return( (count_occurrences(s, car(slist))) +
			(count_occurrences(s, cdr(slist))) )
	else:
		if (car(slist) == s): return (add1(count_occurrences(s, cdr(slist))))
		else: return (count_occurrences(s, cdr(slist)))


# returns a list of the symbols contained in slist in the order in which they occur
# when slist is printed. Intuitively, flatten removes all the inner brackets
# from its argument.
def flatten(slist):
	if isNull(slist): return []
	if isList(car(slist)):
		return( merge(flatten(car(slist)), flatten(cdr(slist))) )
	else:
		return(cons(car(slist), flatten(cdr(slist))))

# returns a list of the elements of list_of_numbers in order of pred
# example:
# >>> sort_lon(isNum_GT, [8, 2, 5, 2, 3])
# [8, 5, 3, 2, 2]
# >>> sort_lon(isNum_LT, [8, 2, 5, 2, 3])
# [2, 2, 3, 5, 8]
def sort_lon(pred, list_of_numbers):
	if isNull(list_of_numbers): return []
	else: return (insert_lon(pred, car(list_of_numbers), sort_lon(pred, cdr(list_of_numbers))))

#########################################################################
# quick sort a list.  The list can be numbers or characters or strings.
# example:
# d = [2, 8, 7, 1, 3, 5, 6, 4]
# >>> qsort_lst(d, len(d)-1)
# >>> d
# [1, 2, 3, 4, 5, 6, 7, 8]
#########################################################################
def partition(lst, r, p):
	x = lst[r]
	i = p - 1  
	for j in range(p, r):
		if lst[j] <= x: 
			i = i + 1
			tmp = lst[i]
			lst[i] = lst[j]
			lst[j] = tmp
	i = i + 1
	tmp = lst[i]
	lst[i] = lst[r]
	lst[r] = tmp
	return i

def qsort_lst(lst, r, p = 0):
	if p < r:
		q = partition(lst, r, p)
		qsort_lst(lst, q - 1, p)
		qsort_lst(lst, r, q + 1)

#########################################################################
