import 'dart:convert';

import 'package:flutter/material.dart';

Widget buildImageFromBase64(String base64String, double width, double height) {
    try {
      // Decode base64 string to bytes
      final bytes = base64Decode(base64String);
      return Image.memory(
        bytes,
        width: width,
        height: height,
        fit: BoxFit.cover,
        // Show a placeholder image until the base64 string is decoded
        errorBuilder: (context, error, stackTrace) {
          return Image.asset(
            'assets/images/no-image-placeholder.png',
            width: width,
            height: height,
            fit: BoxFit.cover,
          );
        },
      );
    } catch (e) {
      // If decoding fails, show placeholder
      return Image.asset(
        'assets/images/no-image-placeholder.png',
        width: width,
        height: height,
        fit: BoxFit.cover,
      );
    }
  }