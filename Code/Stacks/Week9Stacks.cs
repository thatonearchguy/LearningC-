using System;
using System.Collections.Generic;

namespace Code.Stacks
{
    class Week9Stacks
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ShuntingAlgorithm("4+2/(9−3)"));
        }
        public static string ShuntingAlgorithm(string input)
        {
            var workQueue = new Queue<string>();
            var workStack = new Stack<string>();
            var operators = new List<string>{"+", "-", "x", "/", "(", ")"};
            foreach(var token in input)
            {
                if (char.IsDigit(token)) workQueue.Add(token.ToString());
                else 
                {
                    if (workStack.GetPointer() == -1) workStack.Push(token.ToString());
                    else
                    {
                        string operation = workStack.Peek(workStack.GetPointer());
                        if (operators.IndexOf(token.ToString()) < 4)
                        {
                            while (operators.IndexOf(operation) > operators.IndexOf(token.ToString()))
                            {
                                workQueue.Add(workStack.Pop());
                                operation = workStack.Peek(workStack.GetPointer());
                            }
                        }
                        var existingindex = operators.IndexOf(workStack.Pop());
                        var tokenindex = operators.IndexOf(token.ToString());
                        if (tokenindex > 1 && tokenindex < 4 && existingindex < 2)
                        {
                            workQueue.Add(operators[existingindex]);
                            workStack.Push(token.ToString());
                        } 
                        else if (tokenindex == 4) workStack.Push(token.ToString());
                        else if (tokenindex == 5)
                        {
                            if (!workStack.IsEmpty()) {
                                var checkBracket = workStack.Pop();
                                while (operators.IndexOf(checkBracket) != 4)
                                {
                                    workQueue.Add(workStack.Pop());
                                    checkBracket = workStack.Peek(workStack.GetPointer());
                                }
                            }
                        }
                        else workStack.Push(operators[existingindex]);
                    }
                }
            }  
            var checkOperator = workStack.Pop();
            if (char.IsDigit(char.Parse(checkOperator))){}
            else {
                while (operators.IndexOf(checkOperator) > 0)
                {
                    workQueue.Add(checkOperator);
                    checkOperator = workStack.Pop();
                }
            }
            return workQueue.PrintQueue();
        }
        public static int RPN(string input)
        {
            var anotherStack = new Stack<string>();
            foreach (var element in input)
            {
                if (char.IsDigit(element)) anotherStack.Push(element.ToString());
                else
                {
                    var num1 = Int32.Parse(anotherStack.Pop());
                    var num2 = Int32.Parse(anotherStack.Pop());
                    switch (element)
                    {
                        case 'x':
                            anotherStack.Push((num2*num1).ToString());
                            break;
                        case '/':
                            anotherStack.Push((num2/num1).ToString());
                            break;
                        case '+':
                            anotherStack.Push((num2+num1).ToString());
                            break;
                        case '-':
                            anotherStack.Push((num2-num1).ToString());
                            break;
                    }
                }
            }
            return Int32.Parse(anotherStack.Pop());
        }
    }
    class Queue <T>
    {
        List<T> queue = new List<T>();
        public bool isEmpty()
        {
            if (queue.Count == 0) return true;
            else return false;
        }
        public T getFront()
        {
            return queue[0];
        }
        public void Add(T value)
        {
            queue.Add(value);
        }
        public T Dequeue()
        {
            if(isEmpty()) throw new System.InvalidOperationException("Queue is empty!");
            else 
            {
                var values = queue[0];
                queue.RemoveAt(0);
                return values;
            }
        }
        public string PrintQueue()
        {
            return String.Join("", queue);
        }
        public void clear()
        {
            foreach (T value in queue)
            {
                queue.Remove(value);
            }
        }
        public Queue()
        {

        }
    }
    class Stack <T>
    {
        static int maxSize = 1000;
        T[] stack = new T[maxSize];
        int stackPointer;
        public Stack()
        {
            stackPointer = -1;
        }
        public bool Push(T value)
        {
            if (IsFull()) return false;
            else{ 
                stack[stackPointer+=1] = value;
                return true;
            }
        }
        public T Pop()
        {
            if (IsEmpty()) throw new System.InvalidOperationException("Stack is empty!");
            else{
                T value = stack[stackPointer];
                stack[stackPointer] = default(T);
                stackPointer -= 1;
                return value;
            }
        }
        public bool IsEmpty()
        {
            if (stackPointer == -1) return true;
            else return false;
        }
        public T Peek(int pointer)
        {
            return stack[pointer+1];
        } 
        public int GetPointer()
        {
            return stackPointer;
        }
        public bool IsFull()
        {
            if (stackPointer > maxSize) return true;
            else return false;
        }

    }
}