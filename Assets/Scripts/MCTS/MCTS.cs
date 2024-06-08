using System;
using System.Collections.Generic;
using UnityEngine;

public class MCTS
{
    public static Move FindBestMove(GameState initialState, float maxThinkTime)
    {
        MCTSNode rootNode = new MCTSNode(initialState, null);
        float startTime = Time.time;

        while (Time.time - startTime < maxThinkTime)
        {
            MCTSNode node = SelectBestNode(rootNode);
            ExpandNode(node);
            double result = SimulateRandomPlayout(node.gameState);
            Backpropagate(node, result);
        }

        return GetBestMove(rootNode);
    }

    private static MCTSNode SelectBestNode(MCTSNode node)
    {
        MCTSNode bestNode = null;
        double bestValue = double.MinValue;
        foreach (var child in node.children)
        {
            double uctValue = (child.value / (child.visitCount + 1e-6)) +
                              Math.Sqrt(2 * Math.Log(node.visitCount + 1) / (child.visitCount + 1e-6));
            if (uctValue > bestValue)
            {
                bestValue = uctValue;
                bestNode = child;
            }
        }
        return bestNode ?? node;
    }

    private static void ExpandNode(MCTSNode node)
    {
        var legalMoves = GameLogic.GetLegalMoves(node.gameState);
        foreach (var move in legalMoves)
        {
            var newState = GameLogic.ApplyMove(node.gameState, move);
            var childNode = new MCTSNode(newState, move, node);
            node.children.Add(childNode);
        }
    }

    private static double SimulateRandomPlayout(GameState state)
    {
        var currentState = state;
        while (!GameLogic.IsTerminal(currentState))
        {
            var legalMoves = GameLogic.GetLegalMoves(currentState);
            var randomMove = legalMoves[UnityEngine.Random.Range(0, legalMoves.Count)];
            currentState = GameLogic.ApplyMove(currentState, randomMove);
        }
        return GameLogic.EvaluateTerminalState(currentState);
    }

    private static void Backpropagate(MCTSNode node, double result)
    {
        var currentNode = node;
        while (currentNode != null)
        {
            currentNode.visitCount++;
            currentNode.value += result;
            currentNode = currentNode.parent;
        }
    }

    private static Move GetBestMove(MCTSNode rootNode)
    {
        MCTSNode bestNode = null;
        double bestValue = double.MinValue;
        foreach (var child in rootNode.children)
        {
            if (child.visitCount > bestValue)
            {
                bestValue = child.visitCount;
                bestNode = child;
            }
        }
        return bestNode?.move;
    }
}
