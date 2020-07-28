﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
  public PlayerStats stats;

  private InputController controller;
  private Rigidbody2D rb;
  private bool isGrounded = true;

  void Start() {
    this.controller = this.GetComponent<InputController>();
    this.rb = this.GetComponent<Rigidbody2D>();
    this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    this.rb.gravityScale = this.stats.gravityScale;
  }

  void FixedUpdate() {
    this.transform.Translate(Vector3.right * this.controller.x * this.stats.movementSpeed * Time.fixedDeltaTime);

    if (this.isGrounded && this.controller.jump) {
      this.rb.velocity = new Vector2(0f, 0f);
      this.rb.AddForce(Vector2.up * this.stats.jumpForce, ForceMode2D.Impulse);
      this.isGrounded = false;
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    this.isGrounded = (collider.gameObject.layer == LayerMask.NameToLayer("Floor")) || this.isGrounded;
  }
}
