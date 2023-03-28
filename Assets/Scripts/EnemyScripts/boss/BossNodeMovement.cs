using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNodeMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] nodes;
    private int currentNode = 0;

    void Start() {
        // Find the positions of the nodes
        nodes = new Transform[5];
        nodes[0] = GameObject.Find("node1").transform;
        nodes[1] = GameObject.Find("node2").transform;
        nodes[2] = GameObject.Find("node3").transform;
        nodes[3] = GameObject.Find("node4").transform;
        nodes[4] = GameObject.Find("node5").transform;
    }

    void Update() {
        // Move towards the current node
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, speed * Time.deltaTime);

        // Check if we've reached the current node
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.1f) {
            // Move to the next node
            currentNode = (currentNode + 1) % nodes.Length;

            // If we've reached a corner node, wait for 5 seconds before moving to the next node
            if (currentNode == 1 || currentNode == 3) {
                StartCoroutine(WaitForDelay(5f));
            }

            // If we've reached the last node, shuffle the order of the nodes
            if (currentNode == 0) {
                ShuffleNodes();
            }
        }
    }

    IEnumerator WaitForDelay(float delayTime) {
        yield return new WaitForSeconds(delayTime);
    }

    void ShuffleNodes() {
        // Shuffle the order of the nodes randomly
        for (int i = 0; i < nodes.Length; i++) {
            int randomIndex = Random.Range(i, nodes.Length);
            Transform temp = nodes[i];
            nodes[i] = nodes[randomIndex];
            nodes[randomIndex] = temp;
        }
    }
}
