using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concurrency : MonoBehaviour
{
    private void Start() {
        StartCoroutine(DoSomethingAfaterPhysicUpdate());
        /*int[] numbers = new[] { 1, 2, 3 };
        //List<GameObject>
        //HashSet<>
        foreach (var number in numbers) {
            Debug.Log(number);
        }*/
        MyNode nodes = new MyNode("Root");
        nodes.Children.Add(new MyNode("First child"));
        nodes.Children.Add(new MyNode("Second child"));
        foreach (MyNode singleNode in nodes) {
            Debug.Log(singleNode.Data);
        }
        var myEnumberable = YieldMeSomething();
        myEnumberable.Reset();
        myEnumberable.MoveNext();
        /*foreach (var number in myEnumberable) {
            Debug.Log(number);
        }*/
    }
    
    private void Update() {
    }
    private void FixedUpdate() {
        //Debug.Log("Before physics update");
    }
    private void AfterFixedUpdate() {
        //Debug.Log("After physics update");
    }
    private IEnumerator DoSomethingAfaterPhysicUpdate() {
        Debug.Log("Started corroutine");
        while (true) {
            yield return new WaitForFixedUpdate();
            AfterFixedUpdate();
            Debug.Break();
        }
    }
    private IEnumerator YieldMeSomething() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(10f);
        yield return null;
        yield return new WaitForFixedUpdate();
    }
    private IEnumerable<int> GenerateEnumerable() {
        yield return 5;
    }

    class MyNode : IEnumerable<MyNode>, IEnumerable {
        public List<MyNode> Children = new List<MyNode>();
        public string Data { get; private set; }
        public MyNode(string data) {
            Data = data;
        }
        IEnumerator<MyNode> IEnumerable<MyNode>.GetEnumerator() {
            return Children.GetEnumerator();
        }
        public IEnumerator GetEnumerator() {
            return Children.GetEnumerator();
        }
    }

    class MyNodeIterator : IEnumerator<MyNode> {
        private readonly MyNode parentNode;
        private int state;
        private MyNode current;
        public MyNodeIterator(MyNode collection) {
            parentNode = collection;
            state = 0;
        }

        public MyNodeIterator() {
        }

        public bool MoveNext() {
            if (state >= parentNode.Children.Count) {
                current = parentNode.Children[state];
                ++state;
                return true;
            } return false;
        }
        public void Reset() {
            throw new System.NotImplementedException();
        }

        public void Dispose() {
            throw new System.NotImplementedException();
        }

        public object Current => current;

        MyNode IEnumerator<MyNode>.Current => throw new System.NotImplementedException();
    }

}
