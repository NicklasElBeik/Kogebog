import 'package:flutter/material.dart';

class ModernButton extends StatelessWidget {
  final String text;
  final VoidCallback? onPressed;
  final Color backgroundColor;
  final Color foregroundColor;
  final double? width;

  const ModernButton({
    super.key,
    required this.text,
    required this.onPressed,
    this.backgroundColor = const Color(0xFF1e453e),
    this.foregroundColor = Colors.white,
    this.width
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: width, // Full-width button
      child: FilledButton(
        onPressed: onPressed,
        style: FilledButton.styleFrom(
          backgroundColor: backgroundColor,
          foregroundColor: foregroundColor,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12), // Rounded corners
          ),
          elevation: 4, // Soft shadow for depth
          padding: const EdgeInsets.symmetric(vertical: 10, horizontal: 25),
          textStyle: Theme.of(context).textTheme.bodyLarge,
        ),
        child: Text(text),
      ),
    );
  }
}
